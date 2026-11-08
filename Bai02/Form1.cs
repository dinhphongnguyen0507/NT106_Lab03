using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Bai02
{
    public partial class Form1 : Form
    {
        private Socket listenerSocket;
        private Thread serverThread;
        private bool isListening = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            btnListen.Enabled = false;
            isListening = true;
            serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        private void StartUnsafeThread()
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            try
            {
                listenerSocket.Bind(ipepServer);
                listenerSocket.Listen(10);
                AddLog("Telnet running on 127.0.0.1 : 8080");
                while (isListening)
                {
                    Socket clientSocket = listenerSocket.Accept();
                    AddLog("New client connected from" + clientSocket.RemoteEndPoint.ToString());
                    Thread clientThread = new Thread(() => ReceiveData(clientSocket));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                AddLog("Server Stopped");
            }
        }
        private void ReceiveData(Socket clientSocket)
        {
            byte[] recv = new byte[1];
            string text = "";

            while (clientSocket.Connected && isListening)
            {
                try
                {
                    text = "";
                    do
                    {
                        int bytesReceived = clientSocket.Receive(recv);
                        if (bytesReceived == 0) break;


                        text += Encoding.ASCII.GetString(recv, 0, bytesReceived);

                    } while (text.Length > 0 && text[text.Length - 1] != '\n');

                    if (!string.IsNullOrEmpty(text))
                    {

                        AddLog(text.TrimEnd('\r', '\n'));
                    }
                }
                catch
                {
                    break;
                }
            }

            AddLog("Client disconnected.");
            clientSocket.Close();
        }


        private void AddLog(string message)
        {
            if (rtbCommand.InvokeRequired)
            {
                rtbCommand.Invoke(new Action<string>(AddLog), message);
            }
            else
            {
                rtbCommand.AppendText(message + Environment.NewLine);
                rtbCommand.ScrollToCaret();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isListening = false;
            if (listenerSocket != null)
            {
                listenerSocket.Close();
                listenerSocket = null;
            }

            if (serverThread != null && serverThread.IsAlive)
            {
                serverThread.Join(300);
            }
        }
    }
}
