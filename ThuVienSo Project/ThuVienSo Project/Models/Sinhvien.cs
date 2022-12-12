using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Sinhvien
    {
        public Sinhvien()
        {
            Taikhoans = new HashSet<Taikhoan>();
            Thongkes = new HashSet<Thongke>();
        }

        public string Masinhvien { get; set; }
        public int Idkhoa { get; set; }
        public string Hoten { get; set; }
        public string Lop { get; set; }
        public string Gioitinh { get; set; }

        public virtual KhoaBm IdkhoaNavigation { get; set; }
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
        public virtual ICollection<Thongke> Thongkes { get; set; }
    }
}
