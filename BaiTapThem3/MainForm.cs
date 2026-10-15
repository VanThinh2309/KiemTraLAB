using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace BaiTapThem3
{
    public partial class MainForm : Form
    {
        public List<SinhVien> dssv = new List<SinhVien>();
        public QuanLySinhVien qlsv = new QuanLySinhVien();
        //public bool isDataChanged = false;
        public readonly string filePath = "DSSV.txt";
        private string maSoDangSua = null;

        public MainForm()
        {
            InitializeComponent();
        }
        public void MacDinh()
        {
            mtxtMSSV.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtHinh.Clear();
            mtxtSDT.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            rdNam.Checked = true;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            picHinh.Image = null;
            maSoDangSua = null;
            mtxtMSSV.Focus();
        }
        public void HienThiDanhSach()
        {
            dgvSinhVien.Rows.Clear();
            foreach (SinhVien sv in qlsv.dssv)
            {
                dgvSinhVien.Rows.Add(
             sv.MaSo,
             sv.HoTen,
             sv.Phai ? "Nam" : "Nữ",
             sv.NgaySinh.ToString("dd/MM/yyyy"),
             sv.Lop,
             sv.SoDienThoai,
             sv.Email,
             sv.DiaChi,
             sv.Hinh
                );

            }
        }
        public SinhVien GetSinhVienTuForm()
        {
            return new SinhVien
            {
                MaSo = mtxtMSSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                Phai = rdNam.Checked,
                Lop = cboLop.Text,
                SoDienThoai = mtxtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                Hinh = txtHinh.Text.Trim()
            };
        }
        public SinhVien GetSinhVienTuDgv(DataGridViewRow row)
        {
            if (row == null || row.Cells[0].Value == null) return null;

            SinhVien sv = new SinhVien();
            sv.MaSo = row.Cells[0].Value?.ToString() ?? "";
            sv.HoTen = row.Cells[1].Value?.ToString() ?? "";
            sv.Phai = (row.Cells[2].Value?.ToString() == "Nam");

            if (DateTime.TryParseExact(row.Cells[3].Value?.ToString(), "dd/MM/yyyy",
         CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ns))
                sv.NgaySinh = ns;
            else
                sv.NgaySinh = DateTime.Now;

            sv.Lop = row.Cells[4].Value?.ToString() ?? "";
            sv.SoDienThoai = row.Cells[5].Value?.ToString() ?? "";
            sv.Email = row.Cells[6].Value?.ToString() ?? "";
            sv.DiaChi = row.Cells[7].Value?.ToString() ?? "";
            sv.Hinh = row.Cells[8].Value?.ToString() ?? "";

            return sv;
        }
        public void ThietLapControl(SinhVien sv)
        {
            if (sv == null) return;

            mtxtMSSV.Text = sv.MaSo;
            txtHoTen.Text = sv.HoTen;
            dtpNgaySinh.Value = sv.NgaySinh;

            if (sv.Phai)
                rdNam.Checked = true;
            else
                rdNu.Checked = true;

            cboLop.Text = sv.Lop;
            mtxtSDT.Text = sv.SoDienThoai;
            txtEmail.Text = sv.Email;
            txtDiaChi.Text = sv.DiaChi;
            txtHinh.Text = sv.Hinh;

            // Load ảnh lên PictureBox nếu tồn tại file
            if (!string.IsNullOrEmpty(sv.Hinh) && File.Exists(sv.Hinh))
            {
                picHinh.Image = Image.FromFile(sv.Hinh);
            }
            else
            {
                picHinh.Image = null;
            }
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSinhVien.Rows.Count - 1)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
                SinhVien sv = GetSinhVienTuDgv(row);
                ThietLapControl(sv);
                maSoDangSua = sv?.MaSo;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MacDinh();
            qlsv.DocTuFile(filePath);

            HienThiDanhSach();
        }

        private void rdThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void rdMacDinh_Click(object sender, EventArgs e)
        {
            MacDinh();
        }

        private void rdLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mtxtMSSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã sinh viên và Họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SinhVien sv = GetSinhVienTuForm();

            // Kiểm tra xem đã có sinh viên trong danh sách chưa
            if (qlsv.TimTheoMa(sv.MaSo) != null)
            {
                // Nếu ĐÃ TỒN TẠI -> Tiến hành SỬA
                qlsv.CapNhat(sv);
                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Nếu CHƯA TỒN TẠI -> Tiến hành THÊM MỚI
                qlsv.Them(sv);
                MessageBox.Show("Thêm mới sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            qlsv.GhiVaoFile(filePath);
            //isDataChanged = true;
            HienThiDanhSach();
            MacDinh();
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Hình ảnh (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            ofd.Title = "Chọn ảnh sinh viên";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtHinh.Text = ofd.FileName;
                if (File.Exists(txtHinh.Text))
                {
                    picHinh.Image = Image.FromFile(txtHinh.Text);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên trong bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSo = dgvSinhVien.CurrentRow.Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(maSo)) return;
            DialogResult dr = MessageBox.Show( $"Bạn có chắc chắn muốn xóa sinh viên có mã {maSo}?","Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
               
                qlsv.Xoa(maSo);
                qlsv.GhiVaoFile(filePath);
                //isDataChanged = true;
                HienThiDanhSach(); 
                MacDinh();
            }
        }
    }
}
