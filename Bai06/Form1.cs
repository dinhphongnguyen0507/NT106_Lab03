namespace Bai06
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            FormServer serverForm = new FormServer();
            serverForm.Show();
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            FormClient clientForm = new FormClient();
            clientForm.Show();
        }
    }
}
