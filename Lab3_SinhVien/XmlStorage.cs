using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab3_SinhVien
{
    public class XmlStorage : IDataStorage
    {
        public List<SinhVien> Read(string filePath)
        {
            if (!File.Exists(filePath)) return new List<SinhVien>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<SinhVien>));
            using (StreamReader sr = new StreamReader(filePath))
            {
                return (List<SinhVien>)serializer.Deserialize(sr) ?? new List<SinhVien>();
            }
        }

        public void Save(string filePath, List<SinhVien> list)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<SinhVien>));
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                serializer.Serialize(sw, list);
            }
        }
    }
}
