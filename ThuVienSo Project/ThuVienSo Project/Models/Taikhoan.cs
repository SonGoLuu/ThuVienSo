using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Taikhoan
    {
        public string Username { get; set; }
        public string Passwords { get; set; }
        public int Loaiaccount { get; set; }
        public string Magv { get; set; }
        public string Masinhvien { get; set; }

        public virtual Giangvien MagvNavigation { get; set; }
        public virtual Sinhvien MasinhvienNavigation { get; set; }
    }
}
