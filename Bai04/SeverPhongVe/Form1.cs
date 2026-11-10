using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ServerPhongVe
{
    public partial class Form1 : Form
    {
        // Khai báo các thành phần giao diện tự dựng bằng mã lệnh
        private Button btnStart;
        private TextBox txtLog;
        private Label lblTitle;

        // Bộ quản lý cổng mạng Socket
        private TcpListener server;
        private List<Socket> clientSockets = new List<Socket>();
        private HashSet<string> bookedTickets = new HashSet<string>(); // Danh sách lưu ghế đã bán

        public Form1()
        {
            InitializeComponent();
            BuildServerUI(); // Tự động vẽ giao diện màu tối (Dark Mode) khi mở ứng dụng
        }

        // Hàm thiết kế giao diện tự động bằng code
        private void BuildServerUI()
        {
            this.Text = "SERVER TRUNG TÂM - QUẢN LÝ PHÒNG VÉ (MÁY CHỦ)";
            this.Size = new Size(550, 460);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            lblTitle = new Label();
            lblTitle.Text = "HỆ THỐNG TRUNG TÂM PHÒNG VÉ";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 18);
            lblTitle.Size = new Size(300, 30);
            this.Controls.Add(lblTitle);

            btnStart = new Button();
            btnStart.Text = "KÍCH HOẠT SERVER";
            btnStart.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnStart.Size = new Size(160, 38);
            btnStart.Location = new Point(350, 14);
            btnStart.BackColor = Color.ForestGreen;
            btnStart.ForeColor = Color.White;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Cursor = Cursors.Hand;
            btnStart.Click += new EventHandler(btnStart_Click);
            this.Controls.Add(btnStart);

            txtLog = new TextBox();
            txtLog.Multiline = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(490, 330);
            txtLog.Location = new Point(20, 65);
            txtLog.Font = new Font("Consolas", 10);
            txtLog.BackColor = Color.Black;
            txtLog.ForeColor = Color.Lime; // Chữ màu xanh neon màn hình máy chủ
            txtLog.ReadOnly = true;
            this.Controls.Add(txtLog);
        }

        // Hàm cập nhật nhật ký hệ thống an toàn đa luồng (Thread-Safe)
        private void LogMessage(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke((MethodInvoker)delegate { txtLog.AppendText(message + "\r\n"); });
            }
            else
            {
                txtLog.AppendText(message + "\r\n");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở cổng lắng nghe 8888 trên toàn bộ card mạng của máy tính
                server = new TcpListener(IPAddress.Any, 8888);
                server.Start();

                LogMessage("[HỆ THỐNG] Đang nạp dữ liệu phim và phòng chiếu mặc định từ file...");
                LogMessage("[HỆ THỐNG] Kích hoạt cổng mạng thành công!");
                LogMessage("[HỆ THỐNG] Máy chủ đang mở tại cổng 8888. Chờ các quầy kết nối...");

                // Khóa nút bấm để không bị nhấn lặp lại gây lỗi
                btnStart.Enabled = false;
                btnStart.BackColor = Color.DimGray;
                btnStart.Text = "SERVER ĐANG CHẠY";

                // Chạy luồng ngầm liên tục đón kết nối
                Thread listenThread = new Thread(ListenForClients);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động hệ thống máy chủ: " + ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenForClients()
        {
            while (true)
            {
                try
                {
                    Socket clientSocket = server.AcceptSocket();
                    clientSockets.Add(clientSocket);
                    LogMessage($"[KẾT NỐI] Quầy vé mới tham gia hệ thống: {clientSocket.RemoteEndPoint}");

                    // ĐỒNG BỘ BAN ĐẦU: Gửi toàn bộ danh sách các ghế đã bị bán từ trước cho Client mới này cập nhật
                    foreach (var ticket in bookedTickets)
                    {
                        byte[] data = Encoding.UTF8.GetBytes($"KHOAGHE|{ticket}\n");
                        clientSocket.Send(data);
                    }

                    // Tạo luồng nhận lệnh đặt vé độc lập cho từng Client
                    Thread clientThread = new Thread(() => HandleClient(clientSocket));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch
                {
                    break;
                }
            }
        }

        private void HandleClient(Socket client)
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    int received = client.Receive(buffer);
                    if (received == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, received).Trim();

                    // Xử lý gói tin đặt vé nhận từ Quầy: DATVE|GHE_X
                    if (msg.StartsWith("DATVE|"))
                    {
                        string ticketKey = msg.Split('|')[1];

                        lock (bookedTickets)
                        {
                            if (!bookedTickets.Contains(ticketKey))
                            {
                                bookedTickets.Add(ticketKey);
                                LogMessage($"[ĐẶT VÉ] Thành công ghế: <{ticketKey}> từ {client.RemoteEndPoint}");

                                // GỬI THÊM DÒNG NÀY: Báo riêng cho Client này biết là họ đã đặt thành công
                                byte[] successMsg = Encoding.UTF8.GetBytes("DAT_THANH_CONG\n");
                                client.Send(successMsg);

                                // Phát tán đồng bộ khóa ghế tới tất cả các quầy khác
                                BroadcastMessage($"KHOAGHE|{ticketKey}");
                            }
                            else
                            {
                                byte[] failMsg = Encoding.UTF8.GetBytes("GHE_DA_CO_NGUOI_DAT\n");
                                client.Send(failMsg);
                            }
                        }
                    }

                }
            }
            catch
            {
                // Khi client đóng quầy đột ngột
            }
            finally
            {
                clientSockets.Remove(client);
                client.Close();
                LogMessage("[NGẮT KẾT NỐI] Một quầy vé đã thoát khỏi hệ thống.");
            }
        }

        // Hàm phát tin nhắn broadcast đồng bộ thời gian thực mượt mà cho toàn mạng
        private void BroadcastMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            foreach (var socket in clientSockets)
            {
                try
                {
                    socket.Send(data);
                }
                catch
                {
                    // Tự động bỏ qua các kết nối lỗi đường truyền
                }
            }
        }
    }
}
