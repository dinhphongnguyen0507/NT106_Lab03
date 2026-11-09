using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Bai05
{

    public partial class FormClient : Form
    {
        TcpClient tcp = new TcpClient();
        StreamWriter _ghi;
        StreamReader doc;
        public FormClient()
        {
            InitializeComponent();
            this.FormClosing += FormClient_FormClosing;
        }

        private void listMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                await tcp.ConnectAsync("127.0.0.1", 9100);
                var luong = tcp.GetStream();
                _ghi = new StreamWriter(luong) { AutoFlush = true };
                doc = new StreamReader(luong);
                _ = NhanLienTuc(doc);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được server: ");
            }
        }

        private async Task NhanLienTuc(StreamReader doc)
        {
            try
            {
                string? dong;
                while ((dong = await doc.ReadLineAsync()) != null)
                {
                    GoiTin? newPack;
                    try { newPack = JsonSerializer.Deserialize<GoiTin>(dong); }
                    catch (JsonException) { continue; }
                    if (newPack != null) XuLyPack(newPack);
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
        }

        private void XuLyPack(GoiTin pack)
        {
            if (pack.Loai == "MON")
            {
                var item = new ListViewItem(pack.idMon.ToString());
                item.SubItems.Add(pack.TenMon ?? "");
                listMonAn.Items.Add(item);
                listMonAn.EnsureVisible(listMonAn.Items.Count - 1);
            }
            if (pack.Loai == "KETQUA")
            {
                MonTxtBox.Text = pack.TenMon;
                hoTenTxtBox.Text = pack.TenNguoi;
            }
        }

        private void MonTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void hoTenTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void tenMonDongGopTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void tenNguoiDongGopTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private async void randomBtn_Click(object sender, EventArgs e)
        {
            if (_ghi == null)
            {
                MessageBox.Show("Chưa kết nối server");
                return;
            }
            await _ghi.WriteLineAsync(JsonSerializer.Serialize(new GoiTin("RANDOM")));
        }

        private async void DongGopBtn_Click(object sender, EventArgs e)
        {
            if (_ghi == null)
            {
                MessageBox.Show("Chưa kết nối server");
                return;
            }
            var goi = new GoiTin("THEM", TenMon: tenMonDongGopTxt.Text, TenNguoi: tenNguoiDongGopTxt.Text);
            await _ghi.WriteLineAsync(JsonSerializer.Serialize(goi));
            tenMonDongGopTxt.Clear();
            tenMonDongGopTxt.Focus();
        }
        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcp?.Dispose();
        }
    }
}
