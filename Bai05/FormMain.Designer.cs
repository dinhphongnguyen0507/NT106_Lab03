namespace Bai05
{
    partial class FormMain
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
            clientBtn = new Button();
            serverBtn = new Button();
            SuspendLayout();
            // 
            // clientBtn
            // 
            clientBtn.Location = new Point(104, 89);
            clientBtn.Name = "clientBtn";
            clientBtn.Size = new Size(139, 113);
            clientBtn.TabIndex = 0;
            clientBtn.Text = "Chạy client";
            clientBtn.UseVisualStyleBackColor = true;
            clientBtn.Click += clientBtn_Click;
            // 
            // serverBtn
            // 
            serverBtn.Location = new Point(499, 89);
            serverBtn.Name = "serverBtn";
            serverBtn.Size = new Size(139, 113);
            serverBtn.TabIndex = 1;
            serverBtn.Text = "Chạy server";
            serverBtn.UseVisualStyleBackColor = true;
            serverBtn.Click += serverBtn_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(serverBtn);
            Controls.Add(clientBtn);
            Name = "FormMain";
            Text = "Form1";
            Load += FormMain_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button clientBtn;
        private Button serverBtn;
    }
}
