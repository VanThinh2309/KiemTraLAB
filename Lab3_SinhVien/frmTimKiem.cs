using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;

namespace Lab3_SinhVien
{
    public partial class frmTimKiem : Form
    {
        public string TuKhoa { get; private set; }
        public string TieuChi { get; private set; }
       
        public frmTimKiem()
        {
            InitializeComponent();
        }
       
        private void btnTim_Click(object sender, EventArgs e)
        {
            TuKhoa = txtTuKhoa.Text.Trim();
            if(string.IsNullOrEmpty(TuKhoa))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuKhoa.Focus();
                return;
            }
            if(rdbMSSV.Checked) {
                TieuChi = "MSSV";
            }
            else if(rdbHoTen.Checked) {
                TieuChi = "HoTen";
            }
            else if(rdbLop.Checked) {
                TieuChi = "Lop";
            }else if(rdbMonHoc.Checked)
            {
                TieuChi = "MonHoc";
            }
            else {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
