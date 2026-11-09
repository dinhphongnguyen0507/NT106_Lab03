namespace Bai06
{
    partial class FormServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblPort = new Label();
            lblServerStatus = new Label();
            lblActivity = new Label();
            txtPort = new TextBox();
            btnStartServer = new Button();
            lstServerLog = new ListBox();
            SuspendLayout();
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("Segoe UI", 12F);
            lblPort.Location = new Point(37, 83);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(52, 28);
            lblPort.TabIndex = 0;
            lblPort.Text = "Port:";
            // 
            // lblServerStatus
            // 
            lblServerStatus.AutoSize = true;
            lblServerStatus.Location = new Point(37, 146);
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(135, 20);
            lblServerStatus.TabIndex = 1;
            lblServerStatus.Text = "Status: Not running";
            // 
            // lblActivity
            // 
            lblActivity.AutoSize = true;
            lblActivity.Font = new Font("Segoe UI", 11F);
            lblActivity.Location = new Point(37, 200);
            lblActivity.Name = "lblActivity";
            lblActivity.Size = new Size(131, 25);
            lblActivity.TabIndex = 2;
            lblActivity.Text = "Server Activity";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(95, 87);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(510, 27);
            txtPort.TabIndex = 3;
            txtPort.Text = "8080";
            // 
            // btnStartServer
            // 
            btnStartServer.Location = new Point(638, 87);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(123, 48);
            btnStartServer.TabIndex = 4;
            btnStartServer.Text = "START SERVER";
            btnStartServer.UseVisualStyleBackColor = true;
            btnStartServer.Click += btnStartServer_Click;
            // 
            // lstServerLog
            // 
            lstServerLog.FormattingEnabled = true;
            lstServerLog.Location = new Point(37, 244);
            lstServerLog.Name = "lstServerLog";
            lstServerLog.Size = new Size(710, 124);
            lstServerLog.TabIndex = 5;
            // 
            // FormServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 377);
            Controls.Add(lstServerLog);
            Controls.Add(btnStartServer);
            Controls.Add(txtPort);
            Controls.Add(lblActivity);
            Controls.Add(lblServerStatus);
            Controls.Add(lblPort);
            Name = "FormServer";
            Text = "FormServer";
            Load += FormServer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPort;
        private Label lblServerStatus;
        private Label lblActivity;
        private TextBox txtPort;
        private Button btnStartServer;
        private ListBox lstServerLog;
    }
}