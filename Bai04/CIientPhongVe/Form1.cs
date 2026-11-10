using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClientPhongVe
{
    public partial class Form1 : Form
    {
        private TcpClient? client;
        private NetworkStream? stream;
        private Label? lblStatus;

        public Form1()
        {
            InitializeComponent();
            BuildClientUI(); // Tự động vẽ sơ đồ 15 ghế phòng chiếu bằng code
            ConnectToServer(); // Tự động kết nối mạng đến Server
        }

        // Hàm tự động dựng sơ đồ phòng vé (3 hàng x 5 cột = 15 ghế)
        private void BuildClientUI()
        {
            this.Text = "QUẦY BÁN VÉ - CLIENT";
            this.Size = new Size(500, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label lblScreen = new Label();
            lblScreen.Text = "=== MÀN ẢNH SÂN KHẤU ===";
            lblScreen.TextAlign = ContentAlignment.MiddleCenter;
            lblScreen.Font = new Font("Arial", 12, FontStyle.Bold);
            lblScreen.BackColor = Color.DarkGray;
            lblScreen.ForeColor = Color.Black;
            lblScreen.Size = new Size(440, 30);
            lblScreen.Location = new Point(20, 15);
            this.Controls.Add(lblScreen);

            lblStatus = new Label();
            lblStatus.Text = "Trạng thái kết nối: Đang kết nối...";
            lblStatus.Font = new Font("Arial", 9, FontStyle.Italic);
            lblStatus.Location = new Point(20, 315);
            lblStatus.Size = new Size(400, 20);
            this.Controls.Add(lblStatus);

            int seatCounter = 1;
            int startX = 40, startY = 70;
            int spacingX = 85, spacingY = 65;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Button btnSeat = new Button();
                    btnSeat.Text = "Ghế " + seatCounter;
                    btnSeat.Font = new Font("Arial", 9, FontStyle.Bold);
                    btnSeat.Size = new Size(75, 50);
                    btnSeat.Location = new Point(startX + (col * spacingX), startY + (row * spacingY));
                    btnSeat.BackColor = Color.LightSkyBlue;
                    btnSeat.FlatStyle = FlatStyle.Flat;
                    btnSeat.Cursor = Cursors.Hand;

                    // Gán định danh duy nhất cho ghế thông qua thuộc tính Tag
                    btnSeat.Tag = "GHE_" + seatCounter;

                    // Đăng ký sự kiện Click chung cho tất cả các ghế
                    btnSeat.Click += new EventHandler(NutGhe_Click);

                    this.Controls.Add(btnSeat);
                    seatCounter++;
                }
            }
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();
                if (lblStatus != null)
                {
                    lblStatus.Text = "Trạng thái: ĐÃ KẾT NỐI ĐẾN SERVER PHÒNG VÉ.";
                    lblStatus.ForeColor = Color.Green;
                }

                Thread listenThread = new Thread(ListenFromServer);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                if (lblStatus != null)
                {
                    lblStatus.Text = "Trạng thái: THẤT BẠI. Hãy mở Server trước!";
                    lblStatus.ForeColor = Color.Red;
                }
                MessageBox.Show("Không kết nối được tới Server: " + ex.Message);
            }
        }

        private void NutGhe_Click(object? sender, EventArgs e)
        {
            if (sender == null || stream == null) return;
            Button btn = (Button)sender;
            if (btn.Tag == null) return;

            string seatId = btn.Tag.ToString()!;

            // 1. Hiển thị hộp thoại hỏi xác nhận trước khi đặt vé
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn đặt {btn.Text} không?",
                "Xác nhận đặt vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // 2. Nếu người dùng chọn "Yes" (Có) thì mới gửi lệnh lên Server để đặt
            if (result == DialogResult.Yes)
            {
                string request = "DATVE|" + seatId;
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(request);
                    stream.Write(data, 0, data.Length);
                }
                catch
                {
                    MessageBox.Show("Lỗi đường truyền, không thể gửi yêu cầu đặt vé!");
                }
            }
            // Nếu chọn "No" (Không), chương trình sẽ bỏ qua và không làm gì cả
        }

        private void ListenFromServer()
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (stream != null)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                    // Trường hợp 1: Nhận lệnh thông báo khóa ghế (áp dụng cho mọi quầy)
                    if (msg.StartsWith("KHOAGHE|"))
                    {
                        string targetSeatTag = msg.Split('|')[1];

                        this.Invoke((MethodInvoker)delegate {
                            foreach (Control ctrl in this.Controls)
                            {
                                if (ctrl is Button && ctrl.Tag != null && ctrl.Tag.ToString() == targetSeatTag)
                                {
                                    ctrl.Enabled = false;
                                    ctrl.BackColor = Color.Crimson; // Khóa ghế hóa màu đỏ
                                    ctrl.ForeColor = Color.White;
                                }
                            }
                        });
                    }
                    // TRƯỜNG HỢP 2 BỔ SUNG: Báo thành công riêng cho quầy vừa bấm
                    else if (msg == "DAT_THANH_CONG")
                    {
                        MessageBox.Show("Bạn đã đặt ghế thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Trường hợp 3: Báo thất bại nếu quầy khác nhanh tay hơn
                    else if (msg == "GHE_DA_CO_NGUOI_DAT")
                    {
                        MessageBox.Show("Rất tiếc! Ghế này vừa có quầy khác nhanh tay đặt mất rồi.");
                    }
                }
            }
            catch
            {
                this.Invoke((MethodInvoker)delegate {
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "Trạng thái: MẤT KẾT NỐI TỚI HỆ THỐNG TRUNG TÂM.";
                        lblStatus.ForeColor = Color.Red;
                    }
                });
            }
        }

    }
}
