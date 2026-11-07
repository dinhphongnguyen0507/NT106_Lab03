namespace Bai03
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(608, 61);
            button1.Name = "button1";
            button1.Size = new Size(158, 60);
            button1.TabIndex = 0;
            button1.Text = "Connect";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(608, 225);
            button2.Name = "button2";
            button2.Size = new Size(158, 60);
            button2.TabIndex = 1;
            button2.Text = "Send";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button3
            // 
            button3.Location = new Point(608, 378);
            button3.Name = "button3";
            button3.Size = new Size(158, 60);
            button3.TabIndex = 2;
            button3.Text = "Disconnect";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(9, 61);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(584, 377);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // FormClient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 453);
            Controls.Add(richTextBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "FormClient";
            Text = "FormClient";
            Load += FormClient_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private RichTextBox richTextBox1;
    }
}