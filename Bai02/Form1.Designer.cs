namespace Bai02
{
    partial class Form1
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
            btnListen = new Button();
            rtbCommand = new RichTextBox();
            SuspendLayout();
            // 
            // btnListen
            // 
            btnListen.Location = new Point(624, 29);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(115, 41);
            btnListen.TabIndex = 0;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // rtbCommand
            // 
            rtbCommand.Location = new Point(59, 101);
            rtbCommand.Name = "rtbCommand";
            rtbCommand.ReadOnly = true;
            rtbCommand.Size = new Size(685, 316);
            rtbCommand.TabIndex = 1;
            rtbCommand.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(rtbCommand);
            Controls.Add(btnListen);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button btnListen;
        private RichTextBox rtbCommand;
    }
}
