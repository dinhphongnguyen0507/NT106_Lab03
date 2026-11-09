namespace Bai05
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
            randomBtn = new Button();
            label1 = new Label();
            label2 = new Label();
            MonTxtBox = new TextBox();
            hoTenTxtBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            tenMonDongGopTxt = new TextBox();
            tenNguoiDongGopTxt = new TextBox();
            DongGopBtn = new Button();
            listMonAn = new ListView();
            ID = new ColumnHeader();
            MonAn = new ColumnHeader();
            SuspendLayout();
            // 
            // randomBtn
            // 
            randomBtn.Location = new Point(45, 24);
            randomBtn.Name = "randomBtn";
            randomBtn.Size = new Size(213, 62);
            randomBtn.TabIndex = 0;
            randomBtn.Text = "Hôm nay ăn gì ?";
            randomBtn.UseVisualStyleBackColor = true;
            randomBtn.Click += randomBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 123);
            label1.Name = "label1";
            label1.Size = new Size(53, 25);
            label1.TabIndex = 1;
            label1.Text = "Món:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 170);
            label2.Name = "label2";
            label2.Size = new Size(130, 25);
            label2.TabIndex = 2;
            label2.Text = "Đóng góp bởi:";
            // 
            // MonTxtBox
            // 
            MonTxtBox.Location = new Point(104, 120);
            MonTxtBox.Name = "MonTxtBox";
            MonTxtBox.Size = new Size(147, 31);
            MonTxtBox.TabIndex = 3;
            MonTxtBox.TextChanged += MonTxtBox_TextChanged;
            // 
            // hoTenTxtBox
            // 
            hoTenTxtBox.Location = new Point(181, 170);
            hoTenTxtBox.Name = "hoTenTxtBox";
            hoTenTxtBox.Size = new Size(150, 31);
            hoTenTxtBox.TabIndex = 4;
            hoTenTxtBox.TextChanged += hoTenTxtBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Red;
            label3.Location = new Point(145, 233);
            label3.Name = "label3";
            label3.Size = new Size(161, 25);
            label3.TabIndex = 5;
            label3.Text = "Đóng góp món ăn";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 294);
            label4.Name = "label4";
            label4.Size = new Size(84, 25);
            label4.TabIndex = 6;
            label4.Text = "Tên món:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(45, 341);
            label5.Name = "label5";
            label5.Size = new Size(180, 25);
            label5.TabIndex = 7;
            label5.Text = "Tên người đóng góp:";
            // 
            // tenMonDongGopTxt
            // 
            tenMonDongGopTxt.Location = new Point(135, 291);
            tenMonDongGopTxt.Name = "tenMonDongGopTxt";
            tenMonDongGopTxt.Size = new Size(150, 31);
            tenMonDongGopTxt.TabIndex = 8;
            tenMonDongGopTxt.TextChanged += tenMonDongGopTxt_TextChanged;
            // 
            // tenNguoiDongGopTxt
            // 
            tenNguoiDongGopTxt.Location = new Point(231, 338);
            tenNguoiDongGopTxt.Name = "tenNguoiDongGopTxt";
            tenNguoiDongGopTxt.Size = new Size(150, 31);
            tenNguoiDongGopTxt.TabIndex = 9;
            tenNguoiDongGopTxt.TextChanged += tenNguoiDongGopTxt_TextChanged;
            // 
            // DongGopBtn
            // 
            DongGopBtn.Location = new Point(135, 407);
            DongGopBtn.Name = "DongGopBtn";
            DongGopBtn.Size = new Size(174, 51);
            DongGopBtn.TabIndex = 10;
            DongGopBtn.Text = "Đóng góp";
            DongGopBtn.UseVisualStyleBackColor = true;
            DongGopBtn.Click += DongGopBtn_Click;
            // 
            // listMonAn
            // 
            listMonAn.Columns.AddRange(new ColumnHeader[] { ID, MonAn });
            listMonAn.Location = new Point(495, 24);
            listMonAn.Name = "listMonAn";
            listMonAn.Size = new Size(321, 434);
            listMonAn.TabIndex = 11;
            listMonAn.UseCompatibleStateImageBehavior = false;
            listMonAn.View = View.Details;
            listMonAn.SelectedIndexChanged += listMonAn_SelectedIndexChanged;
            // 
            // ID
            // 
            ID.Text = "ID";
            // 
            // MonAn
            // 
            MonAn.Text = "Món ăn";
            MonAn.Width = 200;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 576);
            Controls.Add(listMonAn);
            Controls.Add(DongGopBtn);
            Controls.Add(tenNguoiDongGopTxt);
            Controls.Add(tenMonDongGopTxt);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(hoTenTxtBox);
            Controls.Add(MonTxtBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(randomBtn);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
      
        private Button randomBtn;
        private Label label1;
        private Label label2;
        private TextBox MonTxtBox;
        private TextBox hoTenTxtBox;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox tenMonDongGopTxt;
        private TextBox tenNguoiDongGopTxt;
        private Button DongGopBtn;
        private ListView listMonAn;
        private ColumnHeader ID;
        private ColumnHeader MonAn;
    }
}