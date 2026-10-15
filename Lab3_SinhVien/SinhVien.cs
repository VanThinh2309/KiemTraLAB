using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_SinhVien
{
    public class SinhVien
    {
        public string MSSV { get; set; }         
        public string HoTenLot { get; set; }      
        public string Ten { get; set; }           
        public DateTime NgaySinh { get; set; }     
        public string Lop { get; set; }         
        public string SoCMND { get; set; }         
        public string SoDT { get; set; }         
        public string DiaChi { get; set; }        
        public bool GioiTinh { get; set; }        
        public List<string> MonHocDangKy { get; set; } // Danh sách môn học

        public SinhVien()
        {
            MonHocDangKy = new List<string>();
        }

        public SinhVien(string mssv, string hoTenLot, string ten, DateTime ngaySinh,
                        string lop, string cmnd, string soDT, string diaChi,
                        bool gioiTinh, List<string> dsMonHoc)
        {
            this.MSSV = mssv;
            this.HoTenLot = hoTenLot;
            this.Ten = ten;
            this.NgaySinh = ngaySinh;
            this.Lop = lop;
            this.SoCMND = cmnd;
            this.SoDT = soDT;
            this.DiaChi = diaChi;
            this.GioiTinh = gioiTinh;
            this.MonHocDangKy = dsMonHoc ?? new List<string>();
        }
    }
}

