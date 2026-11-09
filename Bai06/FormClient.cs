using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;

namespace Bai06
{
    public partial class FormClient : Form
    {
        private TcpClient? _client;
        private NetworkStream? _stream;
        private StreamWriter? _writer;
        // Đọc dữ liệu từ Server
        private StreamReader? _reader;

        // Thread chuyên nhận tin nhắn
        private Thread? _receiveThread;

        // Trạng thái kết nối
        private volatile bool _isConnected = false;
        public FormClient()
        {
            InitializeComponent();

            // Lựa chọn mặc định: gửi cho tất cả
            cmbRecipient.Items.Clear();

            cmbRecipient.Items.Add("Tất cả");

            cmbRecipient.SelectedIndex = 0;
        }
        private void UpdateOnlineUsers(string userList)
        {
            if (cmbRecipient.IsDisposed ||
                !cmbRecipient.IsHandleCreated)
                return;

            // Nếu đang ở Thread nhận dữ liệu
            if (cmbRecipient.InvokeRequired)
            {
                try
                {
                    cmbRecipient.BeginInvoke(
                        new Action(() => UpdateOnlineUsers(userList))
                    );
                }
                catch (InvalidOperationException)
                {
                    // Form đang đóng
                }

                return;
            }

            // Lưu lựa chọn hiện tại
            string selected =
                cmbRecipient.SelectedItem?.ToString() ?? "Tất cả";

            cmbRecipient.BeginUpdate();

            cmbRecipient.Items.Clear();

            cmbRecipient.Items.Add("Tất cả");

            // Tách danh sách tên người dùng
            if (!string.IsNullOrEmpty(userList))
            {
                string[] names = userList.Split(';');

                foreach (string name in names)
                {
                    if (!string.IsNullOrWhiteSpace(name) &&
                        !string.Equals(
                            name,
                            _myUserName,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        cmbRecipient.Items.Add(name);
                    }
                }
            }

            // Khôi phục lựa chọn cũ nếu vẫn còn
            int index = cmbRecipient.Items.IndexOf(selected);

            cmbRecipient.SelectedIndex = index >= 0 ? index : 0;

            cmbRecipient.EndUpdate();
        }
        private void HandleServerMessage(string message)
        {
            string[] parts = message.Split('|', 3);

            switch (parts[0])
            {
                case "CHAT":
                    if (parts.Length == 3)
                    {
                        ShowMessage(parts[1] + ": " + parts[2]);
                    }
                    break;

                case "USERS":
                    if (parts.Length >= 2)
                    {
                        UpdateOnlineUsers(parts[1]);
                    }
                    break;

                case "INFO":
                    if (parts.Length >= 2)
                    {
                        ShowMessage("* " + parts[1]);
                    }
                    break;

                case "ERROR":
                    if (parts.Length >= 2)
                    {
                        ShowMessage("[Lỗi] " + parts[1]);
                    }
                    break;

                default:
                    ShowMessage(message);
                    break;
                case "PRIVATE":
                    if (parts.Length == 3)
                    {
                        ShowMessage(
                            "[Tin nhắn riêng] " + parts[1] + ": " + parts[2]
                        );
                    }
                    break;
            }
        }

        private void FormClient_Load(object sender, EventArgs e)
        {

        }
        private string _myUserName = "";
        private void UpdateDisconnectedStatus()
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(
                        new Action(UpdateDisconnectedStatus)
                    );
                }
                catch (InvalidOperationException)
                {
                }

                return;
            }

            lblClientStatus.Text = "Disconnected";

            lblClientStatus.ForeColor = Color.Red;

            btnConnect.Enabled = true;

            btnSend.Enabled = false;
        }
        private void ShowMessage(string message)
        {
            // Kiểm tra Form đã đóng chưa
            if (rtbMessages.IsDisposed ||
                !rtbMessages.IsHandleCreated)
                return;

            // Nếu đang ở Thread khác UI Thread
            if (rtbMessages.InvokeRequired)
            {
                try
                {
                    rtbMessages.BeginInvoke(
                        new Action(() => ShowMessage(message))
                    );
                }
                catch (InvalidOperationException)
                {
                    // Form đang đóng
                }
            }
            else
            {
                // Hiển thị tin nhắn
                rtbMessages.AppendText(
                    message + Environment.NewLine
                );
            }
        }
        private void ReceiveMessages()
        {
            try
            {
                while (_isConnected && _reader != null)
                {
                    // Chờ Server gửi tin nhắn
                    string? message = _reader.ReadLine();

                    // Server đã đóng kết nối
                    if (message == null)
                        break;

                    // Hiển thị tin nhắn
                    HandleServerMessage(message);   
                }
            }
            catch (IOException)
            {
                // Kết nối bị gián đoạn
            }
            catch (ObjectDisposedException)
            {
                // Reader đã được đóng
            }
            finally
            {
                _isConnected = false;

                UpdateDisconnectedStatus();
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _isConnected = false;

            // Đóng kết nối để Thread đang ReadLine() thoát ra
            _client?.Close();

            _reader?.Dispose();

            _writer?.Dispose();

            base.OnFormClosed(e);
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string ip = txtServerIP.Text.Trim();

            if (!int.TryParse(txtPort.Text.Trim(), out int port)
                || port < 1 || port > 65535)
            {
                MessageBox.Show("Port không hợp lệ!");
                return;
            }

            string username = txtUserName.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                username.IndexOfAny(new char[] { '|', ';', '\r', '\n' }) >= 0)
            {
                MessageBox.Show("Vui lòng nhập tên người dùng hợp lệ!");
                return;
            }

            try
            {
                // 1. Tạo TCP Client
                _client = new TcpClient();

                // 2. Kết nối đến Server
                _client.Connect(ip, port);

                // 3. Lấy luồng dữ liệu
                _stream = _client.GetStream();

                // 4. Tạo Reader
                _reader = new StreamReader(
                    _stream,
                    Encoding.UTF8,
                    false,
                    1024,
                    true
                );

                // 5. Tạo Writer
                _writer = new StreamWriter(
                    _stream,
                    new UTF8Encoding(false),
                    1024,
                    true
                );

                _writer.AutoFlush = true;

                // 6. Lưu tên người dùng
                _myUserName = username;

                // 7. Đánh dấu kết nối
                _isConnected = true;

                // 8. Gửi yêu cầu tham gia
                _writer.WriteLine("JOIN|" + _myUserName);

                // 9. Tạo Thread nhận dữ liệu
                _receiveThread = new Thread(ReceiveMessages);
                _receiveThread.IsBackground = true;
                _receiveThread.Start();

                // 10. Cập nhật giao diện
                lblClientStatus.Text = "Connected";
                lblClientStatus.ForeColor = Color.Green;

                btnConnect.Enabled = false;
                btnSend.Enabled = true;

                rtbMessages.AppendText(
                    $"Connected to {ip}:{port}\n"
                );
            }
            catch (Exception ex)
            {
                _isConnected = false;

                _client?.Close();
                _client = null;

                MessageBox.Show(
                    "Không thể kết nối đến Server: " + ex.Message
                );
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
                return;

            if (!_isConnected || _writer == null)
            {
                MessageBox.Show("Bạn chưa kết nối Server!");
                return;
            }

            string recipient =
                cmbRecipient.SelectedItem?.ToString() ?? "Tất cả";

            try
            {
                if (recipient == "Tất cả")
                {
                    // Gửi tin nhắn chung
                    _writer.WriteLine("CHAT|" + message);

                    rtbMessages.AppendText(
                        "You: " + message + Environment.NewLine
                    );
                }
                else
                {
                    // Gửi tin nhắn riêng
                    _writer.WriteLine(
                        "PRIVATE|" + recipient + "|" + message
                    );

                    rtbMessages.AppendText(
                        "[Riêng tư đến " + recipient + "] You: "
                        + message + Environment.NewLine
                    );
                }

                txtMessage.Clear();
                txtMessage.Focus();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Lỗi gửi tin nhắn: " + ex.Message);
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("Kết nối đã bị đóng!");
            }
        }

    }
}
