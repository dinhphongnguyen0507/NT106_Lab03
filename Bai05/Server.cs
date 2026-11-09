using System;
using System.Collections.Concurrent;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.Data.Sqlite;
namespace Bai05
{
    public class Server
    {
        private const string chuoiSQL = @"Data Source=C:\Users\admin\source\repos\lab02IOSTREAM\lab02IOSTREAM\bin\Debug\net10.0-windows\Food.db";
        private readonly ConcurrentDictionary<string, StreamWriter> _danhsachClient = new();
        private readonly SemaphoreSlim _khoaDb = new(1, 1);
        private readonly SemaphoreSlim _khoaGui = new(1, 1);
        private async Task GuiCho1Client(StreamWriter ghi, GoiTin Pack)
        {
            string dong = JsonSerializer.Serialize(Pack);
            await _khoaGui.WaitAsync();
            try
            {
                await ghi.WriteLineAsync(dong);
            }
            catch (IOException) { }
            finally
            {
                _khoaGui.Release();
            }
        }

        private async Task PhatAllClient(GoiTin pack, string? tru)
        {
            foreach (var (id, ghi) in _danhsachClient)
            {
                if (id == tru) continue;
                await GuiCho1Client(ghi, pack);
            }
        }

        private async Task<long> LuuMonAn(string monan, string hoTen)
        {
            await _khoaDb.WaitAsync();
            try
            {
                SqliteConnection sqlCon = new SqliteConnection(chuoiSQL);
                await sqlCon.OpenAsync();
                long id;
                using (var timKiem = new SqliteCommand("SELECT IDND FROM NGUOIDUNG WHERE HOTEN = @hoTen LIMIT 1", sqlCon))
                {
                    timKiem.Parameters.AddWithValue("@hoTen", hoTen);
                    object kq = await timKiem.ExecuteScalarAsync();
                    if (kq != null) id = Convert.ToInt64(kq);
                    else
                    {
                        using (var cmdHoTen = new SqliteCommand("INSERT INTO NGUOIDUNG (HOTEN) VALUES (@hoten)", sqlCon))
                        {
                            cmdHoTen.Parameters.AddWithValue("@hoten", hoTen);
                            await cmdHoTen.ExecuteNonQueryAsync();
                        }
                        using var lay = new SqliteCommand("SELECT last_insert_rowid()", sqlCon);
                        id = (long)(await lay.ExecuteScalarAsync())!;
                    }
                }
                var chuoiCMD = " INSERT INTO MONAN (TENMONAN,IDND) VALUES (@monan,@id)";
                using (var cmd = new SqliteCommand(chuoiCMD, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@monan", monan);
                    cmd.Parameters.AddWithValue("@id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
                using var layMon = new SqliteCommand("SELECT last_insert_rowid()", sqlCon);
                return (long)(await layMon.ExecuteScalarAsync())!;
            }
            finally { _khoaDb.Release(); }
        }

        public async Task SendDanhSachMonAn(StreamWriter doc)
        {
            var list = new List<GoiTin>();
            await _khoaDb.WaitAsync();
            try
            {
                using var openSQL = new SqliteConnection(chuoiSQL);
                await openSQL.OpenAsync();
                var cmd = "SELECT IDMN , TENMONAN FROM MONAN";
                var r = new SqliteCommand(cmd, openSQL);
                using var exc = await r.ExecuteReaderAsync();
                while (await exc.ReadAsync())
                    list.Add((new GoiTin("MON", idMon: exc.GetInt64(0), TenMon: exc.GetString(1))));
                Console.WriteLine($"Đọc được {list.Count} món từ DB");
            }
            finally
            {
                _khoaDb.Release();
            }
            foreach (var dong in list)
            {
                await GuiCho1Client(doc, dong);
            }
        }

        public async Task phucvuAsync(TcpClient tcp)
        {
            string id = Guid.NewGuid().ToString();
            using (tcp)
            {
                var doc = new StreamReader(tcp.GetStream());
                var ghi = new StreamWriter(tcp.GetStream()) { AutoFlush = true };
                _danhsachClient[id] = ghi;
                await SendDanhSachMonAn(ghi);
                Console.WriteLine($"[{id}]) vào -> Tổng cộng {_danhsachClient.Count} Client kết nối ! ");
                try
                {
                    await GuiCho1Client(ghi, new GoiTin("ID:", IDND: id));
                    string docYeuCauClient;
                    while ((docYeuCauClient = await doc.ReadLineAsync()) != null)
                    {
                        GoiTin? yc = JsonSerializer.Deserialize<GoiTin>(docYeuCauClient);
                        if (yc.Loai == "RANDOM")
                        {
                            await (GuiCho1Client(ghi, await RandomMonAn()));
                            continue;
                        }
                        if (yc.Loai == "THEM")
                        {
                            await (XuLyThem(yc, ghi));
                            continue;
                        }
                    }
                }
                catch (IOException) { }
                finally
                {
                    _danhsachClient.TryRemove(id, out _);
                    Console.WriteLine($"[{id}] đã rời phòng ! Còn {_danhsachClient.Count}");
                }
            }
        }

        private async Task XuLyThem(GoiTin yc, StreamWriter ghi)
        {
            string tenMon = yc.TenMon;
            string TenNguoiDongGop = yc.TenNguoi;
            if (tenMon == "" || TenNguoiDongGop == "")
            {
                await GuiCho1Client(ghi, new GoiTin("LOI", ThongBao: "Vui lòng nhập đầy đủ"));
                return;
            }
            long IDMN = await LuuMonAn(tenMon, TenNguoiDongGop);
            Console.WriteLine($"[{TenNguoiDongGop}] đã thêm 1 món ăn thành công!");
            await PhatAllClient(new GoiTin("MON", idMon: IDMN, TenMon: tenMon), null);
        }
        public async Task<GoiTin> RandomMonAn()
        {
            await _khoaDb.WaitAsync();
            try
            {
                var openSQL = new SqliteConnection(chuoiSQL);
                await openSQL.OpenAsync();
                var cmd = @"
                    SELECT m.TENMONAN, n.HOTEN 
                    FROM MONAN m 
                    JOIN NGUOIDUNG n ON m.IDND = n.IDND 

                    ORDER BY RANDOM() 
                    LIMIT 1";

                var r = new SqliteCommand(cmd, openSQL);
                using var exc = await r.ExecuteReaderAsync();
                await exc.ReadAsync();
                return new GoiTin("KETQUA", TenMon: exc.GetString(0), TenNguoi: exc.GetString(1));
            }
            finally
            {
                _khoaDb.Release();
            }
        }
        public async Task ChayAsync()
        {
            var listener = new TcpListener(IPAddress.Any, 9100);
            listener.Start();
            Console.WriteLine("Server đang chờ ở cổng 9100...");

            while (true)
            {
                TcpClient tcp = await listener.AcceptTcpClientAsync();
                _ = phucvuAsync(tcp);
            }
        }
    }
}
