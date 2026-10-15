using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab3_JSON_HuongDanThucHanh
{
    public partial class frmReadJsonFile : Form
    {
        public frmReadJsonFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Str = "";
            string Path = "../../student.json";//Đường dẫn đến file JSON
            List<StudentInfo> list = LoadJSON(Path);
            for(int i = 0 ; i < list.Count ; i++)
            {
                StudentInfo info = list[i];
                Str += string.Format("Sinh viên {0} có MSSV: {1}, họ tên: {2}, " + "điểm TB: {3}\r\n", (i + 1), info.MSSV, info.Hoten, info.DiemTB); 
            }
            MessageBox.Show(Str);
                
        }
        private List<StudentInfo> LoadJSON(string Path)
        {
            //Khai báo danh sách lưu trữ
            List<StudentInfo> List = new List<StudentInfo>();
            StreamReader r = new StreamReader(Path);
            // Dùng using để tự động đóng file sau khi đọc
           
                string json = r.ReadToEnd();//Đọc hết
                var array = (JObject)JsonConvert.DeserializeObject(json);
                var students = array["sinhvien"].Children();

                foreach (var item in students)
                {
                    string mssv = item["MSSV"].Value<string>();
                    string hoten = item["hoten"].Value<string>();
                    int tuoi = item["tuoi"].Value<int>();
                    double diem = item["diem"].Value<double>();
                    bool tongiao = item["tongiao"].Value<bool>();
                //Chuyển vào đối tượng StudentInfo
                StudentInfo info = new StudentInfo(mssv, hoten, tuoi, diem, tongiao);
                    List.Add(info);
                }
            return List;
        }
    }

}
