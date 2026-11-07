using System;
using System.Threading;
using System.Windows.Forms;

namespace Bai03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormServer server = new FormServer();
            server.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormClient client = new FormClient();
            client.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FormClient client = new FormClient();
            client.Show();
        }
    }
}