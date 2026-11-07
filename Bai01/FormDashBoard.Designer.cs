namespace Bai01
{
    partial class FormDashBoard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOpenServer = new Button();
            btnOpenClient = new Button();
            SuspendLayout();
            // 
            // btnOpenServer
            // 
            btnOpenServer.Location = new Point(78, 129);
            btnOpenServer.Name = "btnOpenServer";
            btnOpenServer.Size = new Size(203, 122);
            btnOpenServer.TabIndex = 0;
            btnOpenServer.Text = "UDP Server";
            btnOpenServer.UseVisualStyleBackColor = true;
            btnOpenServer.Click += btnOpenServer_Click;
            // 
            // btnOpenClient
            // 
            btnOpenClient.Location = new Point(519, 129);
            btnOpenClient.Name = "btnOpenClient";
            btnOpenClient.Size = new Size(203, 122);
            btnOpenClient.TabIndex = 1;
            btnOpenClient.Text = "UDP Client";
            btnOpenClient.UseVisualStyleBackColor = true;
            btnOpenClient.Click += btnOpenClient_Click;
            // 
            // FormDashBoard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOpenClient);
            Controls.Add(btnOpenServer);
            Name = "FormDashBoard";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenServer;
        private Button btnOpenClient;
    }
}
