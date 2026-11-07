using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace Bai01
{
    public partial class FormServer : Form
    {
        private UdpClient udpServer;
        private Thread listenThread;
        private bool isListening = false;

        public FormServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(txtPort.Text.Trim());
                udpServer = new UdpClient(port);
                isListening = true;
                listenThread = new Thread(new ThreadStart(ListenForData));
                listenThread.IsBackground = true;
                listenThread.Start();
                btnListen.Enabled = false;
                txtPort.Enabled = false;
                AddMessage($"[System] Sever đang lắng nghe tại cổng {port}...");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo Server:" + ex.Message);
            }
        }
        private void ListenForData()
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (isListening)
            {
                try
                {
                    byte[] receiveByte = udpServer.Receive(ref remoteEndPoint);
                    string returnData = Encoding.UTF8.GetString(receiveByte);
                    string formattedMessage = $"{remoteEndPoint.Address} : {remoteEndPoint.Port} : {returnData}";
                    AddMessage(formattedMessage);
                }
                catch (SocketException ex)
                {
                    break;
                }
            }
        }
        private void AddMessage(string msg)
        {
            if (rtbMessage.InvokeRequired)
            {
                rtbMessage.Invoke(new Action<string>(AddMessage), msg);
            }
            else
            {
                rtbMessage.AppendText(msg + Environment.NewLine);
                rtbMessage.ScrollToCaret();
            }
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            if (udpServer != null)
            {
                udpServer.Close();
                udpServer = null;
            }
            if (listenThread != null && listenThread.IsAlive)
            {
                listenThread.Join(500);
            }
        }
    }
}
