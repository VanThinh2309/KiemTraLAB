using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapThem3
{
    public class QuanLySinhVien
    {
        public List<SinhVien> dssv;
        public QuanLySinhVien()
        {
            dssv = new List<SinhVien>();

        }
        public SinhVien this[int index]
        {
            get { return (SinhVien)dssv[index]; }
            set { dssv[index] = value; }
        }

        public SinhVien TimTheoMa(string maSo)
        {
            foreach (SinhVien sv in dssv)
            {
                if (sv.MaSo.Equals(maSo, StringComparison.OrdinalIgnoreCase))
                    return sv;
            }
            return null;
        }
        public bool CapNhat(SinhVien svMoi)
        {
            SinhVien svCu = TimTheoMa(svMoi.MaSo);
            if (svCu != null)
            {
                int index = dssv.IndexOf(svCu);
                dssv[index] = svMoi; // Gán đối tượng mới đã sửa đè lên vị trí cũ
                return true;
            }
            return false;
        }
        public bool Them(SinhVien sv)
        {
            
            if (TimTheoMa(sv.MaSo) != null)
            {
                return false; // Đã tồn tại mã số này
            }

            dssv.Add(sv);
            return true; // Thêm thành công
        }
        public void Xoa(string maSo)
        {
            SinhVien sv = TimTheoMa(maSo);
            if (sv != null)
            {
                dssv.Remove(sv);
            }
        }
        public void DocTuFile(string filename)
        {
            string t;
            string[] s;
            SinhVien sv;

            if (!File.Exists(filename)) return;

            using (StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read), Encoding.UTF8))
            {
                while ((t = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(t)) continue;

                    s = t.Split('\t');
                    if (s.Length >= 9)
                    {
                        sv = new SinhVien();
                        sv.MaSo = s[0];
                        sv.HoTen = s[1];
                        sv.NgaySinh = DateTime.ParseExact(s[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        sv.Phai = (s[3] == "Nam"); // hoặc s[2] == "1" tùy định dạng file
                        sv.Lop = s[4];
                        sv.SoDienThoai = s[5];
                        sv.Email = s[6];
                        sv.DiaChi = s[7];
                        sv.Hinh = s[8];

                        this.Them(sv); // Thêm sinh viên vào danh sách của lớp Quản lý
                    }
                }
            }
        }
        public void GhiVaoFile(string filePath)
            {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (SinhVien sv in dssv)
                {
                    sw.WriteLine(sv.ToFileString());
                }
            }
        }
    
    }
}
