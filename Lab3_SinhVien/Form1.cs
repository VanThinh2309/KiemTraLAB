using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_SinhVien
{
    public partial class Form1 : Form
    {
        private QLDSSV ql;
        private string currentFilePath = "students.txt"; // Mặc định dùng file TXT (hoặc .json / .xml)

        public Form1()
        {
            InitializeComponent();
            ql = new QLDSSV(currentFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dsMon = ql.DocDanhSachMon();
            if (dsMon != null)
            {
                clbMonHoc.Items.Clear();
                foreach (var m in dsMon) clbMonHoc.Items.Add(m);
            }
            //ql.DocFile();
            HienThiThongTinDGV(ql.dsSinhVien);
        }

        /// <summary>
        /// Hiển thị danh sách sinh viên lên DataGridView / ListView
        /// </summary>
        private void HienThiThongTinDGV(List<SinhVien> danhSach)
        {
            dgvSinhVien.Rows.Clear();
            if (danhSach == null) return;

            foreach (var sv in danhSach)
            {
                int index = dgvSinhVien.Rows.Add(
                    sv.MSSV,
                    sv.HoTenLot,
                    sv.Ten,
                    sv.NgaySinh.ToString("dd/MM/yyyy"),
                    sv.Lop,
                    sv.SoCMND,
                    sv.SoDT,
                    sv.DiaChi,
                    sv.GioiTinh ? "Nam" : "Nữ",
                    string.Join(", ", sv.MonHocDangKy)
                );

                // Gán đối tượng SinhVien vào Tag của dòng để dễ truy xuất
                dgvSinhVien.Rows[index].Tag = sv;
            }
        }

        /// <summary>
        /// Kiểm tra đầy đủ thông tin bắt buộc trước khi Thêm / Cập nhật
        /// </summary>
        private bool KiemTraThongTin()
        {
            // 1. Kiểm tra các ô nhập văn bản
            if (string.IsNullOrWhiteSpace(txtHoTenLot.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(cboLop.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Họ tên, Lớp và Địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra MaskedTextBox MSSV (đủ 7 số)
            if (!mtbMSSV.MaskCompleted && mtbMSSV.Text.Replace(" ", "").Length < 7)
            {
                MessageBox.Show("Mã số sinh viên phải nhập đủ 7 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbMSSV.Focus();
                return false;
            }

            // 3. Kiểm tra MaskedTextBox CMND (đủ 9 số)
            if (!mtbCMND.MaskCompleted && mtbCMND.Text.Replace(" ", "").Length < 9)
            {
                MessageBox.Show("Số CMND/CCCD chưa nhập đủ 9 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbCMND.Focus();
                return false;
            }

            // 4. Kiểm tra MaskedTextBox SĐT (đủ 10 số)
            if (!mtbSDT.MaskCompleted && mtbSDT.Text.Replace(" ", "").Length < 10)
            {
                MessageBox.Show("Số điện thoại chưa nhập đủ 10 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbSDT.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Thu thập đối tượng SinhVien từ giao diện
        /// </summary>
        private SinhVien GetSinhVienFromForm()
        {
            SinhVien sv = new SinhVien
            {
                MSSV = mtbMSSV.Text.Trim(),
                HoTenLot = txtHoTenLot.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                Lop = cboLop.Text.Trim(),
                SoCMND = mtbCMND.Text.Trim(),
                SoDT = mtbSDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                GioiTinh = rdbNam.Checked
            };

            foreach (var item in clbMonHoc.CheckedItems)
            {
                sv.MonHocDangKy.Add(item.ToString());
            }

            return sv;
        }

        /// <summary>
        /// Tự động điền dữ liệu từ dòng được chọn lên các Control phía trên
        /// </summary>
        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow == null || dgvSinhVien.CurrentRow.Tag == null)
                return;

            SinhVien sv = (SinhVien)dgvSinhVien.CurrentRow.Tag;
            mtbMSSV.Text = sv.MSSV;
            txtHoTenLot.Text = sv.HoTenLot;
            txtTen.Text = sv.Ten;
            dtpNgaySinh.Value = sv.NgaySinh;
            cboLop.Text = sv.Lop;
            mtbCMND.Text = sv.SoCMND;
            mtbSDT.Text = sv.SoDT;
            txtDiaChi.Text = sv.DiaChi;

            if (sv.GioiTinh)
                rdbNam.Checked = true;
            else
                rdbNu.Checked = true;

            // Bỏ chọn tất cả môn học hiện tại 
            for (int i = 0; i < clbMonHoc.Items.Count; i++)
                clbMonHoc.SetItemChecked(i, false);

            // Tick chọn lại các môn đăng ký của sinh viên
            for (int i = 0; i < clbMonHoc.Items.Count; i++)
            {
                string mon = clbMonHoc.Items[i].ToString();
                if (sv.MonHocDangKy.Contains(mon))
                    clbMonHoc.SetItemChecked(i, true);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!KiemTraThongTin()) return;

            // Kiểm tra định dạng MSSV theo Lớp (AABBCCC)
            string error;
            if (!ql.KiemTraMSSVHopLe(mtbMSSV.Text.Trim(), cboLop.Text.Trim(), out error))
            {
                MessageBox.Show(error, "Lỗi định dạng MSSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbMSSV.Focus();
                return;
            }

            SinhVien sv = GetSinhVienFromForm();
            if (ql.Them(sv))
            {
                HienThiThongTinDGV(ql.dsSinhVien);
               
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("MSSV đã tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraThongTin()) return;

            // Kiểm tra MSSV có bị đổi so với dòng đang chọn không
            if (dgvSinhVien.CurrentRow != null && dgvSinhVien.CurrentRow.Tag is SinhVien svDangChon)
            {
                if (mtbMSSV.Text.Trim() != svDangChon.MSSV)
                {
                    MessageBox.Show("Không được đổi MSSV khi cập nhật!\nNếu là sinh viên mới, hãy dùng nút Thêm mới.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mtbMSSV.Text = svDangChon.MSSV;   // trả lại mã cũ
                    mtbMSSV.Focus();
                    return;
                }
            }

            SinhVien sv = GetSinhVienFromForm();
            if (ql.CapNhat(sv))
            {
                HienThiThongTinDGV(ql.dsSinhVien);
                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã sinh viên để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region XỬ LÝ CONTEXT MENU MÔN HỌC (CheckedListBox)
        private void LuuDanhSachMon()
        {
            ql.GhiDanhSachMon(clbMonHoc.Items.Cast<string>());
        }
        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string monMoi = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên môn học mới:", "Thêm môn học mới");
            if (!string.IsNullOrWhiteSpace(monMoi) && !clbMonHoc.Items.Contains(monMoi.Trim()))
            {
                clbMonHoc.Items.Add(monMoi.Trim(), true);
                LuuDanhSachMon();
            }
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clbMonHoc.SelectedIndex == -1) return;

            string mon = clbMonHoc.SelectedItem.ToString();
            int soSV = ql.DemSinhVienDangKy(mon);

            string msg = soSV > 0
                ? $"Có {soSV} sinh viên đã đăng ký môn \"{mon}\".\nXóa môn này sẽ gỡ môn khỏi các sinh viên đó. Tiếp tục?"
                : $"Bạn có chắc muốn xóa môn \"{mon}\"?";

            if (MessageBox.Show(msg, "Xác nhận xóa môn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            ql.GoMonHoc(mon);
            clbMonHoc.Items.RemoveAt(clbMonHoc.SelectedIndex);
            LuuDanhSachMon();
            HienThiThongTinDGV(ql.dsSinhVien);
        }

        private void clbMonHoc_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = clbMonHoc.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    clbMonHoc.SelectedIndex = index;
                }
            }
        }
        #endregion

        #region XỬ LÝ CONTEXT MENU XÓA SINH VIÊN TRÊN BẢNG
        private void xóaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<string> dsXoa = new List<string>();

            // Lấy danh sách các dòng đang chọn trên DataGridView
            foreach (DataGridViewRow row in dgvSinhVien.SelectedRows)
            {
                if (row.Tag is SinhVien sv)
                {
                    dsXoa.Add(sv.MSSV);
                }
            }

            if (dsXoa.Count == 0 && dgvSinhVien.CurrentRow != null && dgvSinhVien.CurrentRow.Tag is SinhVien svCur)
            {
                dsXoa.Add(svCur.MSSV);
            }

            if (dsXoa.Count > 0)
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa {dsXoa.Count} sinh viên đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ql.Xoa(dsXoa);
                    HienThiThongTinDGV(ql.dsSinhVien);
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (frmTimKiem frm = new frmTimKiem())
            {
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    var ketQua = ql.TimKiemTheoTieuChi(frm.TuKhoa, frm.TieuChi);
                    if (ketQua.Count > 0)
                    {
                        HienThiThongTinDGV(ketQua);
                        MessageBox.Show($"Tìm thấy {ketQua.Count} sinh viên phù hợp với tiêu chí.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên phù hợp với tiêu chí.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HienThiThongTinDGV(ql.dsSinhVien); 
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình hay không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        private void ResetForm()
        {
            HienThiThongTinDGV(ql.dsSinhVien);
            mtbMSSV.Clear();
            txtHoTenLot.Clear();
            txtTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboLop.SelectedIndex = -1;
            cboLop.Text = "";
            mtbCMND.Clear();
            mtbSDT.Clear();
            txtDiaChi.Clear();
            rdbNam.Checked = true; // Đặt mặc định là Nam

            // Bỏ chọn tất cả các môn học
            for (int i = 0; i < clbMonHoc.Items.Count; i++)
            {
                clbMonHoc.SetItemChecked(i, false);
            }
           

        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void dgvSinhVien_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Nếu dòng được click chưa nằm trong danh sách các dòng đang chọn,
                // thì bỏ chọn cũ và chọn dòng mới này
                if (!dgvSinhVien.Rows[e.RowIndex].Selected)
                {
                    dgvSinhVien.ClearSelection();
                    dgvSinhVien.Rows[e.RowIndex].Selected = true;
                }
                ctmsXoaSV.Show(Cursor.Position);
            }
           
        }
    }
}