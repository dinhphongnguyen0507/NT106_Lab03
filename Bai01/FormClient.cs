using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Bai01
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string ipAddress = txtIP.Text.Trim();
            string portText=txtPort.Text.Trim();
            string message = rtbMessage.Text;

            if (string.IsNullOrEmpty(ipAddress) ||  string.IsNullOrEmpty(portText) )
            {
                MessageBox.Show(
                    "Vui lòng nhập IP và Port đầy đủ!",
                    "Cảnh báo", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show(
                    "Vui lòng nhập nội dung tin nhắn!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int port = int.Parse(portText);
                UdpClient udpClient = new UdpClient();
                byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                udpClient.Send(sendBytes, sendBytes.Length, ipAddress,port);
                udpClient.Close();
                rtbMessage.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi gửi dữ liệu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
