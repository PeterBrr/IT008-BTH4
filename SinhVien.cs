using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH4
{
    public class SinhVien
    {
        public string MaSo { get; set; }
        public string Ten { get; set; }
        public string Khoa { get; set; }
        public float DiemTB { get; set; }

        public SinhVien() { }

        public SinhVien(string ma, string ten, string khoa, float diem)
        {
            MaSo = ma;
            Ten = ten;
            Khoa = khoa;
            DiemTB = diem;
        }
    }
}
