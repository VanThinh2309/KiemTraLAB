using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Lab3_SinhVien
{
    public class JsonStorage : IDataStorage
    {
        public List<SinhVien> Read(string filePath)
        {
            if (!File.Exists(filePath)) return new List<SinhVien>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<SinhVien>>(json) ?? new List<SinhVien>();
        }

        public void Save(string filePath, List<SinhVien> list)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
