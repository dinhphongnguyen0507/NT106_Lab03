namespace Bai01
{
    public partial class FormDashBoard : Form
    {
        public FormDashBoard()
        {
            InitializeComponent();
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            FormServer formserver_ = new FormServer();
            formserver_.Show();
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            FormClient formclient_ = new FormClient();
            formclient_.Show();
        }
    }
}
