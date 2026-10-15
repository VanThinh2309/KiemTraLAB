using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_SinhVien
{
    public interface IDataStorage
    {
        List<SinhVien> Read(string filePath);
        void Save(string filePath, List<SinhVien> list);
    }

}
