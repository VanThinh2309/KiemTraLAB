namespace Lab3_SinhVien
{
    partial class frmTimKiem
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
            this.btnTim = new System.Windows.Forms.Button();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.rdbMSSV = new System.Windows.Forms.RadioButton();
            this.rdbLop = new System.Windows.Forms.RadioButton();
            this.rdbMonHoc = new System.Windows.Forms.RadioButton();
            this.rdbHoTen = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnTim
            // 
            this.btnTim.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTim.Location = new System.Drawing.Point(209, 168);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(130, 50);
            this.btnTim.TabIndex = 5;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // txtTuKhoa
            // 
            this.txtTuKhoa.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTuKhoa.Location = new System.Drawing.Point(64, 34);
            this.txtTuKhoa.Multiline = true;
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(444, 39);
            this.txtTuKhoa.TabIndex = 6;
            // 
            // rdbMSSV
            // 
            this.rdbMSSV.AutoSize = true;
            this.rdbMSSV.Checked = true;
            this.rdbMSSV.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdbMSSV.Location = new System.Drawing.Point(64, 112);
            this.rdbMSSV.Name = "rdbMSSV";
            this.rdbMSSV.Size = new System.Drawing.Size(75, 23);
            this.rdbMSSV.TabIndex = 7;
            this.rdbMSSV.TabStop = true;
            this.rdbMSSV.Text = "MSSV";
            this.rdbMSSV.UseVisualStyleBackColor = true;
            // 
            // rdbLop
            // 
            this.rdbLop.AutoSize = true;
            this.rdbLop.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdbLop.Location = new System.Drawing.Point(167, 112);
            this.rdbLop.Name = "rdbLop";
            this.rdbLop.Size = new System.Drawing.Size(59, 23);
            this.rdbLop.TabIndex = 7;
            this.rdbLop.TabStop = true;
            this.rdbLop.Text = "Lớp";
            this.rdbLop.UseVisualStyleBackColor = true;
            // 
            // rdbMonHoc
            // 
            this.rdbMonHoc.AutoSize = true;
            this.rdbMonHoc.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdbMonHoc.Location = new System.Drawing.Point(262, 112);
            this.rdbMonHoc.Name = "rdbMonHoc";
            this.rdbMonHoc.Size = new System.Drawing.Size(98, 23);
            this.rdbMonHoc.TabIndex = 7;
            this.rdbMonHoc.TabStop = true;
            this.rdbMonHoc.Text = "Môn Học";
            this.rdbMonHoc.UseVisualStyleBackColor = true;
            // 
            // rdbHoTen
            // 
            this.rdbHoTen.AutoSize = true;
            this.rdbHoTen.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdbHoTen.Location = new System.Drawing.Point(405, 112);
            this.rdbHoTen.Name = "rdbHoTen";
            this.rdbHoTen.Size = new System.Drawing.Size(103, 23);
            this.rdbHoTen.TabIndex = 7;
            this.rdbHoTen.TabStop = true;
            this.rdbHoTen.Text = "Họ và Tên";
            this.rdbHoTen.UseVisualStyleBackColor = true;
            // 
            // frmTimKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 249);
            this.Controls.Add(this.rdbHoTen);
            this.Controls.Add(this.rdbMonHoc);
            this.Controls.Add(this.rdbLop);
            this.Controls.Add(this.rdbMSSV);
            this.Controls.Add(this.txtTuKhoa);
            this.Controls.Add(this.btnTim);
            this.Name = "frmTimKiem";
            this.Text = "frmTimKiem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.RadioButton rdbMSSV;
        private System.Windows.Forms.RadioButton rdbLop;
        private System.Windows.Forms.RadioButton rdbMonHoc;
        private System.Windows.Forms.RadioButton rdbHoTen;
    }
}