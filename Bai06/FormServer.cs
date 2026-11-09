using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Bai06
{
    public partial class FormServer : Form
    {

        private TcpListener? _listener;


        private Thread? _listenThread;


        private volatile bool _isRunning = false;
        // Lưu tên người dùng tương ứng với từng Client
        private Dictionary<TcpClient, string> _userNames
            = new Dictionary<TcpClient, string>();
        public FormServer()
        {

            InitializeComponent();
        }
        private List<TcpClient> _clients = new List<TcpClient>();
        private readonly object _clientsLock = new object();
        private Dictionary<TcpClient, StreamWriter> _writers
            = new Dictionary<TcpClient, StreamWriter>();

        private void SendToClient(TcpClient client, string message)
        {
            StreamWriter? writer = null;

            lock (_clientsLock)
            {
                _writers.TryGetValue(client, out writer);
            }

            if (writer == null)
                return;

            try
            {
                lock (writer)
                {
                    writer.WriteLine(message);
                }
            }
            catch (IOException)
            {
                // Kết nối bị lỗi
            }
            catch (ObjectDisposedException)
            {
                // Writer đã đóng
            }
        }
        private void SendUserList()
        {
            List<string> names = new List<string>();

            // Sao chép danh sách tên người dùng
            lock (_clientsLock)
            {
                names.AddRange(_userNames.Values);
            }

            // Gửi danh sách cho tất cả Client
            string message = "USERS|" + string.Join(";", names);

            SendToAll(message);
        }
        private void SendToAll(string message, TcpClient? except = null)
        {
            List<StreamWriter> recipients = new List<StreamWriter>();

            // Lấy danh sách Writer của các Client đã đăng ký tên
            lock (_clientsLock)
            {
                foreach (KeyValuePair<TcpClient, StreamWriter> item in _writers)
                {
                    if (item.Key != except && _userNames.ContainsKey(item.Key))
                    {
                        recipients.Add(item.Value);
                    }
                }
            }

            // Gửi dữ liệu
            foreach (StreamWriter writer in recipients)
            {
                try
                {
                    lock (writer)
                    {
                        writer.WriteLine(message);
                    }
                }
                catch (IOException)
                {
                    // Bỏ qua kết nối bị lỗi
                }
                catch (ObjectDisposedException)
                {
                    // Writer đã bị đóng
                }
            }
        }
        private void HandleClient(TcpClient client)
        {
            string remoteAddress =
                client.Client.RemoteEndPoint?.ToString() ?? "Unknown";

            string username = "";
            bool registered = false;

            StreamReader? reader = null;

            try
            {
                NetworkStream stream = client.GetStream();

                reader = new StreamReader(
                    stream,
                    Encoding.UTF8,
                    false,
                    1024,
                    true
                );

                // 1. Chờ Client gửi tên người dùng
                string? joinMessage = reader.ReadLine();

                if (joinMessage == null ||
                    !joinMessage.StartsWith("JOIN|"))
                {
                    SendToClient(client, "ERROR|Thông tin tham gia không hợp lệ.");
                    return;
                }

                // 2. Lấy username
                username = joinMessage.Substring(5).Trim();

                if (string.IsNullOrWhiteSpace(username) ||
                    username.IndexOfAny(new char[] { '|', ';', '\r', '\n' }) >= 0)
                {
                    SendToClient(client, "ERROR|Tên người dùng không hợp lệ.");
                    return;
                }

                // 3. Kiểm tra tên đã có người sử dụng chưa
                bool nameExists = false;

                lock (_clientsLock)
                {
                    foreach (string existingName in _userNames.Values)
                    {
                        if (string.Equals(
                            existingName,
                            username,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            nameExists = true;
                            break;
                        }
                    }

                    if (!nameExists)
                    {
                        _userNames.Add(client, username);
                    }
                }

                if (nameExists)
                {
                    SendToClient(client, "ERROR|Tên người dùng đã được sử dụng.");
                    return;
                }

                registered = true;

                AddLog(username + " đã tham gia phòng chat.");

                // 4. Thông báo người mới tham gia
                SendToAll("INFO|" + username + " đã tham gia phòng chat.");

                // 5. Cập nhật danh sách người online
                SendUserList();

                // 6. Tiếp tục nhận tin nhắn
                // 6. Tiếp tục nhận tin nhắn
                while (_isRunning)
                {
                    string? message = reader.ReadLine();

                    if (message == null)
                        break;

                    // =================================
                    // TRƯỜNG HỢP 1: CHAT CHUNG
                    // =================================

                    if (message.StartsWith("CHAT|", StringComparison.Ordinal))
                    {
                        string chatText = message.Substring(5);

                        string chatMessage =
                            "CHAT|" + username + "|" + chatText;

                        AddLog(username + ": " + chatText);

                        // Gửi đến tất cả Client khác
                        SendToAll(chatMessage, client);
                    }

                    // =================================
                    // TRƯỜNG HỢP 2: CHAT RIÊNG
                    // =================================

                    else if (message.StartsWith("PRIVATE|", StringComparison.Ordinal))
                    {
                        string[] privateParts =
                            message.Split(new char[] { '|' }, 3);

                        if (privateParts.Length != 3)
                            continue;

                        string recipientName = privateParts[1].Trim();

                        string privateText = privateParts[2];

                        TcpClient? recipientClient = null;

                        // Tìm Client có tên người nhận
                        lock (_clientsLock)
                        {
                            foreach (KeyValuePair<TcpClient, string> item in _userNames)
                            {
                                if (string.Equals(
                                    item.Value,
                                    recipientName,
                                    StringComparison.OrdinalIgnoreCase))
                                {
                                    recipientClient = item.Key;
                                    break;
                                }
                            }
                        }

                        // Kiểm tra người nhận có đang online không
                        if (recipientClient == null || recipientClient == client)
                        {
                            SendToClient(
                                client,
                                "ERROR|Người nhận không tồn tại hoặc không hợp lệ."
                            );

                            continue;
                        }

                        // Ghi nhận tin nhắn riêng trên Server
                        AddLog(
                            "[Riêng tư] " + username +
                            " -> " + recipientName + ": " + privateText
                        );

                        // Chỉ gửi cho người nhận
                        SendToClient(
                            recipientClient,
                            "PRIVATE|" + username + "|" + privateText
                        );
                    }
                }
            }
            catch (IOException)
            {
                // Client ngắt kết nối hoặc xảy ra lỗi truyền dữ liệu
            }
            catch (ObjectDisposedException)
            {
                // Kết nối đã bị đóng
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }

                StreamWriter? writer = null;

                // Xóa Client khỏi các danh sách
                lock (_clientsLock)
                {
                    _clients.Remove(client);

                    _userNames.Remove(client);

                    if (_writers.TryGetValue(client, out writer))
                    {
                        _writers.Remove(client);
                    }
                }

                if (writer != null)
                {
                    lock (writer)
                    {
                        writer.Dispose();
                    }
                }

                client.Close();

                if (registered)
                {
                    AddLog(username + " đã rời phòng chat.");

                    // Thông báo và cập nhật danh sách online
                    SendToAll("INFO|" + username + " đã rời phòng chat.");

                    SendUserList();
                }
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _isRunning = false;

            _listener?.Stop();

            lock (_clientsLock)
            {
                foreach (TcpClient client in _clients)
                {
                    client.Close();
                }

                _clients.Clear();
            }

            base.OnFormClosed(e);
        }
        private void AddLog(string message)
        {
            if (lstServerLog.IsDisposed)
                return;

            if (lstServerLog.InvokeRequired)
            {
                if (lstServerLog.IsHandleCreated)
                {
                    lstServerLog.BeginInvoke(
                        new Action(() => AddLog(message))
                    );
                }

                return;
            }

            lstServerLog.Items.Add(
                DateTime.Now.ToString("HH:mm:ss") + " - " + message
            );

            lstServerLog.TopIndex = lstServerLog.Items.Count - 1;
        }
        private void ListenForClients()
        {
            while (_isRunning)
            {
                try
                {
                    
                    TcpClient client = _listener!.AcceptTcpClient();


                    NetworkStream stream = client.GetStream();

                    StreamWriter writer = new StreamWriter(
                        stream,
                        new UTF8Encoding(false),
                        1024,
                        true
                    );

                    writer.AutoFlush = true;

                    writer.AutoFlush = true;


                    string remoteAddress =
                        client.Client.RemoteEndPoint?.ToString() ?? "Unknown";


                    lock (_clientsLock)
                    {
                        _clients.Add(client);
                        _writers.Add(client, writer);
                    }

                    AddLog("Client connected: " + remoteAddress);

                   
                    Thread clientThread =
                        new Thread(() => HandleClient(client));

                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (SocketException ex)
                {
                    if (_isRunning)
                    {
                        AddLog("Socket error: " + ex.Message);
                    }

                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }
        }

        private void FormServer_Load(object sender, EventArgs e)
        {

        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPort.Text.Trim(), out int port)
        || port < 1 || port > 65535)
            {
                MessageBox.Show("Port không hợp lệ!");
                return;
            }
            try
            {

                _listener = new TcpListener(IPAddress.Any, port);

                _listener.Start();

                _isRunning = true;

                lblServerStatus.Text = "Status: Running";

                btnStartServer.Enabled = false;

                AddLog($"Server started on port {port}");


                _listenThread = new Thread(ListenForClients);

                _listenThread.IsBackground = true;

                _listenThread.Start();
            }
            catch (SocketException ex)
            {
                _isRunning = false;
                _listener?.Stop();

                MessageBox.Show(
                    "Không thể khởi động Server: " + ex.Message
                );
            }
        }
    }
}
