using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Bai03
{
    public partial class FormClient : Form
    {
        private TcpClient tcpClient;
        private NetworkStream stream;

        public FormClient()
        {
            InitializeComponent();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient tcpClient = new TcpClient();
                tcpClient = new TcpClient();

                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8080);
                tcpClient.Connect(ipEndPoint);

                stream = tcpClient.GetStream();

                MessageBox.Show("Kết nối đến Server thành công!");
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến Server: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected && stream != null)
                {
                    string message = richTextBox1.Text;
                    if (!string.IsNullOrEmpty(message))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                        stream.Write(data, 0, data.Length);

                        richTextBox1.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập nội dung tin nhắn!");
                    }
                }
                else
                {
                    MessageBox.Show("Chưa kết nối đến Server!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi dữ liệu: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisconnectFromServer();
        }

        private void DisconnectFromServer()
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected)
                {
                    if (stream != null)
                    {
                        byte[] data = Encoding.UTF8.GetBytes("quit\n");
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                    }

                    tcpClient.Close();
                    button1.Enabled = true;
                    MessageBox.Show("Đã ngắt kết nối khỏi Server.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ngắt kết nối: " + ex.Message);
            }
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectFromServer();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient = new TcpClient();

                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8080);
                tcpClient.Connect(ipEndPoint);

                stream = tcpClient.GetStream();

                MessageBox.Show("Kết nối đến Server thành công!");
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến Server: " + ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected && stream != null)
                {
                    string message = richTextBox1.Text;
                    if (!string.IsNullOrEmpty(message))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                        stream.Write(data, 0, data.Length);

                        richTextBox1.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập nội dung tin nhắn!");
                    }
                }
                else
                {
                    MessageBox.Show("Chưa kết nối đến Server!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi dữ liệu: " + ex.Message);
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DisconnectFromServer();
        }
    }
}


