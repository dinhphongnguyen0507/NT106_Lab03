namespace Bai06
{
    partial class FormClient
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
            lblServerIP = new Label();
            lblPort = new Label();
            lblClientStatus = new Label();
            txtServerIP = new TextBox();
            txtMessage = new TextBox();
            txtPort = new TextBox();
            btnConnect = new Button();
            btnSend = new Button();
            rtbMessages = new RichTextBox();
            lblUserName = new Label();
            txtUserName = new TextBox();
            lblRecipient = new Label();
            cmbRecipient = new ComboBox();
            SuspendLayout();
            // 
            // lblServerIP
            // 
            lblServerIP.AutoSize = true;
            lblServerIP.Font = new Font("Microsoft Sans Serif", 10F);
            lblServerIP.Location = new Point(31, 58);
            lblServerIP.Name = "lblServerIP";
            lblServerIP.Size = new Size(83, 20);
            lblServerIP.TabIndex = 0;
            lblServerIP.Text = "Server IP:";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("Segoe UI", 10F);
            lblPort.Location = new Point(473, 55);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(45, 23);
            lblPort.TabIndex = 1;
            lblPort.Text = "Port:";
            // 
            // lblClientStatus
            // 
            lblClientStatus.AutoSize = true;
            lblClientStatus.Location = new Point(31, 134);
            lblClientStatus.Name = "lblClientStatus";
            lblClientStatus.Size = new Size(99, 20);
            lblClientStatus.TabIndex = 2;
            lblClientStatus.Text = "Disconnected";
            // 
            // txtServerIP
            // 
            txtServerIP.Location = new Point(31, 93);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(376, 27);
            txtServerIP.TabIndex = 3;
            txtServerIP.Text = "127.0.0.1";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(31, 409);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(669, 27);
            txtMessage.TabIndex = 4;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(473, 93);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(381, 27);
            txtPort.TabIndex = 5;
            txtPort.Text = "8080";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(706, 149);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(148, 42);
            btnConnect.TabIndex = 6;
            btnConnect.Text = "CONNECT";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSend
            // 
            btnSend.Enabled = false;
            btnSend.Location = new Point(706, 409);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(148, 67);
            btnSend.TabIndex = 7;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rtbMessages
            // 
            rtbMessages.Location = new Point(31, 197);
            rtbMessages.Name = "rtbMessages";
            rtbMessages.Size = new Size(823, 206);
            rtbMessages.TabIndex = 8;
            rtbMessages.Text = "";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(31, 477);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(78, 20);
            lblUserName.TabIndex = 9;
            lblUserName.Text = "Username:";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(115, 477);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(292, 27);
            txtUserName.TabIndex = 10;
            // 
            // lblRecipient
            // 
            lblRecipient.AutoSize = true;
            lblRecipient.Location = new Point(31, 528);
            lblRecipient.Name = "lblRecipient";
            lblRecipient.Size = new Size(63, 20);
            lblRecipient.TabIndex = 11;
            lblRecipient.Text = "Send to:";
            // 
            // cmbRecipient
            // 
            cmbRecipient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRecipient.FormattingEnabled = true;
            cmbRecipient.Location = new Point(115, 528);
            cmbRecipient.Name = "cmbRecipient";
            cmbRecipient.Size = new Size(292, 28);
            cmbRecipient.TabIndex = 12;
            // 
            // FormClient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 593);
            Controls.Add(cmbRecipient);
            Controls.Add(lblRecipient);
            Controls.Add(txtUserName);
            Controls.Add(lblUserName);
            Controls.Add(rtbMessages);
            Controls.Add(btnSend);
            Controls.Add(btnConnect);
            Controls.Add(txtPort);
            Controls.Add(txtMessage);
            Controls.Add(txtServerIP);
            Controls.Add(lblClientStatus);
            Controls.Add(lblPort);
            Controls.Add(lblServerIP);
            Name = "FormClient";
            Text = "FormClient";
            Load += FormClient_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblServerIP;
        private Label lblPort;
        private Label lblClientStatus;
        private TextBox txtServerIP;
        private TextBox txtMessage;
        private TextBox txtPort;
        private Button btnConnect;
        private Button btnSend;
        private RichTextBox rtbMessages;
        private Label lblUserName;
        private TextBox txtUserName;
        private Label lblRecipient;
        private ComboBox cmbRecipient;
    }
}