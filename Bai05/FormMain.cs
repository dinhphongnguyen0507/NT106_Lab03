using System.Diagnostics;

namespace Bai05
{
    public partial class FormMain : Form
    {
        private Process? _tienTrinhServer;
        public FormMain()
        {
            InitializeComponent();
        }

        private void clientBtn_Click(object sender, EventArgs e)
        {
            new FormClient().Show();
        }

        private void serverBtn_Click(object sender, EventArgs e)
        {
            if (_tienTrinhServer != null && !_tienTrinhServer.HasExited)
            {
                MessageBox.Show("Server đang chạy ");
                return;
            }
            var psi = new ProcessStartInfo(Environment.ProcessPath!) { UseShellExecute = false };
            psi.ArgumentList.Add("server");
            _tienTrinhServer = Process.Start(psi);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
