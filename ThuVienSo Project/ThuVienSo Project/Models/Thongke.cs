using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Thongke
    {
        public int Idthongke { get; set; }
        public string Magv { get; set; }
        public int Masach { get; set; }
        public string Masinhvien { get; set; }
        public DateTime? Ngaytai { get; set; }
        public DateTime Ngaydoc { get; set; }

        public virtual Giangvien MagvNavigation { get; set; }
        public virtual Sach MasachNavigation { get; set; }
        public virtual Sinhvien MasinhvienNavigation { get; set; }
    }
}
