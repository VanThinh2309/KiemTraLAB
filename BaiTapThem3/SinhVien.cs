    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapThem3
{
    public class SinhVien
    {
        public string MaSo { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool Phai { get; set; }
        public string Lop { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Hinh { get; set; }

        public SinhVien()
        {

        }
        public SinhVien(string maSo, string hoTen, DateTime ngaySinh, bool phai, string lop, string soDienThoai, string email, string diaChi, string hinh)
        {
            MaSo = maSo;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            Phai = phai;
            Lop = lop;
            SoDienThoai = soDienThoai;
            Email = email;
            DiaChi = diaChi;
            Hinh = hinh;
        }
        public string ToFileString()
        {
            return $"{MaSo}\t{HoTen}\t{NgaySinh:dd/MM/yyyy}\t{(Phai ? "Nam" : "Nữ")}\t{Lop}\t{SoDienThoai}\t{Email}\t{DiaChi}\t{Hinh}";
        }

    }
}
