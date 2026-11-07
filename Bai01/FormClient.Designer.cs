namespace Bai01
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
            label1 = new Label();
            txtIP = new TextBox();
            label2 = new Label();
            txtPort = new TextBox();
            label3 = new Label();
            rtbMessage = new RichTextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 35);
            label1.Name = "label1";
            label1.Size = new Size(115, 20);
            label1.TabIndex = 0;
            label1.Text = "IP Remote Host:";
            // 
            // txtIP
            // 
            txtIP.Location = new Point(22, 67);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(344, 27);
            txtIP.TabIndex = 1;
            txtIP.Text = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(461, 35);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 2;
            label2.Text = "Port:";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(461, 67);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(231, 27);
            txtPort.TabIndex = 3;
            txtPort.Text = "8080";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(68, 125);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.TabIndex = 4;
            label3.Text = "Message:";
            // 
            // rtbMessage
            // 
            rtbMessage.Location = new Point(57, 157);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.Size = new Size(658, 211);
            rtbMessage.TabIndex = 5;
            rtbMessage.Text = "";
            // 
            // btnSend
            // 
            btnSend.Location = new Point(57, 394);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 29);
            btnSend.TabIndex = 6;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // FormClient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSend);
            Controls.Add(rtbMessage);
            Controls.Add(label3);
            Controls.Add(txtPort);
            Controls.Add(label2);
            Controls.Add(txtIP);
            Controls.Add(label1);
            Name = "FormClient";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIP;
        private Label label2;
        private TextBox txtPort;
        private Label label3;
        private RichTextBox rtbMessage;
        private Button btnSend;
    }
}