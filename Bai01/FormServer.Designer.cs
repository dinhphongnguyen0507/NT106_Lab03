namespace Bai01
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
            label1 = new Label();
            txtPort = new TextBox();
            btnListen = new Button();
            rtbMessage = new RichTextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 42);
            label1.Name = "label1";
            label1.Size = new Size(38, 20);
            label1.TabIndex = 0;
            label1.Text = "Port:";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(116, 42);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(303, 27);
            txtPort.TabIndex = 1;
            txtPort.Text = "8080";
            // 
            // btnListen
            // 
            btnListen.Location = new Point(618, 42);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(94, 29);
            btnListen.TabIndex = 2;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // rtbMessage
            // 
            rtbMessage.Location = new Point(54, 120);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.ReadOnly = true;
            rtbMessage.Size = new Size(658, 266);
            rtbMessage.TabIndex = 3;
            rtbMessage.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 97);
            label2.Name = "label2";
            label2.Size = new Size(134, 20);
            label2.TabIndex = 4;
            label2.Text = "Received Message:";
            // 
            // FormServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(rtbMessage);
            Controls.Add(btnListen);
            Controls.Add(txtPort);
            Controls.Add(label1);
            Name = "FormServer";
            Text = "FormServer";
            FormClosing += FormServer_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtPort;
        private Button btnListen;
        private RichTextBox rtbMessage;
        private Label label2;
    }
}