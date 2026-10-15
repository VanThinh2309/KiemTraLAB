using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3_SinhVien
{
    public class QLDSSV
    {
        public List<SinhVien> dsSinhVien;
        private string filePath;
        private IDataStorage storage;

        public QLDSSV(string path = "students.txt")
        {
            dsSinhVien = new List<SinhVien>();
            SetFilePath(path);
        }

        // Thay đổi định dạng file lưu trữ linh hoạt
        public void SetFilePath(string path)
        {
            this.filePath = path;
            string ext = Path.GetExtension(path).ToLower();

            switch (ext)
            {
                case ".json":
                    storage = new JsonStorage();
                    break;
                case ".xml":
                    storage = new XmlStorage();
                    break;
                default:
                    storage = new TxtStorage();
                    break;
            }
            DocFile();
        }

        public void DocFile()
        {
            dsSinhVien = storage.Read(filePath);
        }

        public void GhiFile()
        {
            new TxtStorage().Save("students.txt", dsSinhVien);
            new JsonStorage().Save("students.json", dsSinhVien);
            new XmlStorage().Save("students.xml", dsSinhVien);
        }

        // Validate MSSV dạng AABBCCC dựa vào tên Lớp
        public bool KiemTraMSSVHopLe(string mssv, string tenLop, out string error)
        {
            error = "";
            if (mssv.Length != 7 || !mssv.All(char.IsDigit))
            {
                error = "MSSV phải gồm đúng 7 chữ số!";
                return false;
            }

            // Lấy 2 số cuối của năm nhập học từ Lớp (Ví dụ: CTK44 -> 20, CTK43 -> 19)
            // Hoặc lấy 2 số trong tên lớp nếu lớp dạng "K44" / "CTK44"
            string digitsInClass = new string(tenLop.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(digitsInClass))
            {
                error = "Tên lớp phải chứa khóa (Ví dụ: CTK44)!";
                return false;
            }
            string aa = mssv.Substring(0, 2);
            if (aa != digitsInClass)
            {
                error = "2 số đầu của MSSV phải trùng với khóa của lớp (" + digitsInClass + ")!";
                return false;
            }

            string bb = mssv.Substring(2, 2);
            if (bb != "10")
            {
                error = "MSSV phải có dạng AABBCCC với BB = 10!";
                return false;
            }

            return true;
        }

        public bool Them(SinhVien sv)
        {
            if (dsSinhVien.Any(x => x.MSSV == sv.MSSV))
                return false;

            dsSinhVien.Add(sv);
            GhiFile();
            return true;
        }

        public bool CapNhat(SinhVien sv)
        {
            var svCu = dsSinhVien.FirstOrDefault(x => x.MSSV == sv.MSSV);
            if (svCu == null)
                return false;

            svCu.HoTenLot = sv.HoTenLot;
            svCu.Ten = sv.Ten;
            svCu.NgaySinh = sv.NgaySinh;
            svCu.Lop = sv.Lop;
            svCu.SoCMND = sv.SoCMND;
            svCu.SoDT = sv.SoDT;
            svCu.DiaChi = sv.DiaChi;
            svCu.GioiTinh = sv.GioiTinh;
            svCu.MonHocDangKy = new List<string>(sv.MonHocDangKy ?? new List<string>());

            GhiFile();
            return true;
        }
        public void Xoa(List<string> dsMSSV)
        {
            dsSinhVien.RemoveAll(x => dsMSSV.Contains(x.MSSV));
            GhiFile();
        }
        // Đưa về chữ thường, bỏ khoảng trắng thừa: "  Văn   An " -> "văn an"
        private static string ChuanHoa(string s)
        {
            return string.Join(" ", (s ?? "").ToLower()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }
        // Tìm kiếm sinh viên theo đúng tiêu chí chọn từ frmTimKiem
        public List<SinhVien> TimKiemTheoTieuChi(string tuKhoa, string tieuChi)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
                return dsSinhVien;

            string kw = tuKhoa.Trim().ToLower();

            switch (tieuChi)
            {
                case "MSSV":
                    return dsSinhVien.Where(sv => sv.MSSV != null && sv.MSSV.ToLower().Contains(kw)).ToList();

                case "Lop":
                    return dsSinhVien.Where(sv => sv.Lop != null && sv.Lop.ToLower().Contains(kw)).ToList();

                case "MonHoc":
                    return dsSinhVien.Where(sv => sv.MonHocDangKy != null &&
                                                  sv.MonHocDangKy.Any(m => m.ToLower().Contains(kw))).ToList();

                case "HoTen":
                    {
                        string tk = " " + ChuanHoa(kw) + " ";
                        return dsSinhVien.Where(sv =>
                            (" " + ChuanHoa(sv.HoTenLot + " " + sv.Ten) + " ").Contains(tk)
                        ).ToList();
                    }
                default:
                    return dsSinhVien;
            }
        }
        ////ThẽmoaMon bo sung them
        private const string FileMonHoc = "monhoc.txt";

        // Trả về null nếu chưa có file (để form giữ danh sách mặc định trong Designer)
        public List<string> DocDanhSachMon()
        {
            if (!File.Exists(FileMonHoc)) return null;
            return File.ReadAllLines(FileMonHoc)
                       .Where(s => !string.IsNullOrWhiteSpace(s))
                       .ToList();
        }

        public void GhiDanhSachMon(IEnumerable<string> dsMon)
        {
            File.WriteAllLines(FileMonHoc, dsMon);
        }

        public int DemSinhVienDangKy(string mon)
        {
            return dsSinhVien.Count(sv => sv.MonHocDangKy != null && sv.MonHocDangKy.Contains(mon));
        }

        // Gỡ môn khỏi tất cả sinh viên đã đăng ký, trả về số sinh viên bị ảnh hưởng
        public int GoMonHoc(string mon)
        {
            int dem = 0;
            foreach (var sv in dsSinhVien)
            {
                if (sv.MonHocDangKy != null && sv.MonHocDangKy.Remove(mon)) dem++;
            }
            if (dem > 0) GhiFile();
            return dem;
        }
    }
}

