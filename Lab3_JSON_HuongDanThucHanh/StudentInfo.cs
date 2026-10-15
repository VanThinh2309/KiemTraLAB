using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_JSON_HuongDanThucHanh
{
    internal class StudentInfo
    {
        public string MSSV { get; set; }
        public string Hoten { get; set; }
        public int Tuoi { get; set; }
        public double DiemTB { get; set; }
        public bool TonGiao { get; set; }
        public StudentInfo() { }
        public StudentInfo(string mssv, string hoten, int tuoi, double diemTB, bool tonGiao)
        {
            MSSV = mssv;
            Hoten = hoten;
            Tuoi = tuoi;
            DiemTB = diemTB;
            TonGiao = tonGiao;
        }
        
    }
}
