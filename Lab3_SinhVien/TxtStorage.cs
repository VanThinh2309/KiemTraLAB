using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab3_SinhVien
{
    public class TxtStorage : IDataStorage
    {
        public List<SinhVien> Read(string filePath)
        {
            var list = new List<SinhVien>();

            if (!File.Exists(filePath)) return list;
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts.Length < 9) continue;

                var sv = new SinhVien
                {
                    MSSV = parts[0],
                    HoTenLot = parts[1],
                    Ten = parts[2],
                    NgaySinh = System.DateTime.Parse(parts[3]),
                    Lop = parts[4],
                    SoCMND = parts[5],
                    SoDT = parts[6],
                    DiaChi = parts[7],
                    GioiTinh = bool.Parse(parts[8])
                };

                if (parts.Length > 9 && !string.IsNullOrEmpty(parts[9]))
                {
                    sv.MonHocDangKy = new List<string>(parts[9].Split(';'));
                }

                list.Add(sv);
            }
            return list;
        }
        public void Save(string filePath, List<SinhVien> list)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var sv in list)
                {
                    string dsMon = string.Join(";", sv.MonHocDangKy ?? new List<string>());
                    string line = $"{sv.MSSV}|{sv.HoTenLot}|{sv.Ten}|{sv.NgaySinh:yyyy-MM-dd}|{sv.Lop}|{sv.SoCMND}|{sv.SoDT}|{sv.DiaChi}|{sv.GioiTinh}|{dsMon}";
                    sw.WriteLine(line);
                }
            }
        }
    }
}
