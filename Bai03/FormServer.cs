using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai03
{
    public partial class FormServer : Form
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private bool isListening = false;

        public FormServer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void FormServer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
                tcpListener.Start();

                isListening = true;
                richTextBox1.AppendText("Server started!\n");
                button1.Enabled = false;

                listenThread = new Thread(new ThreadStart(StartUnsafeThread));
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi chạy Server: " + ex.Message);
            }
        }

        private void StartUnsafeThread()
        {
            try
            {
                while (isListening)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    richTextBox1.AppendText($"Connection accepted from {client.Client.RemoteEndPoint}\n");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        richTextBox1.AppendText($"From client: {data}");
                    }

                    client.Close();
                }
            }
            catch (SocketException)
            {
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText($"Lỗi: {ex.Message}\n");
            }
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
                tcpListener.Start();

                isListening = true;
                richTextBox1.AppendText("Server started!\n");
                button1.Enabled = false;

                listenThread = new Thread(new ThreadStart(StartUnsafeThread));
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi chạy Server: " + ex.Message);
            }
        }
    }
}