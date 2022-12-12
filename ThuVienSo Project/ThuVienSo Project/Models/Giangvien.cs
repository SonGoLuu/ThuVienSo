using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Giangvien
    {
        public Giangvien()
        {
            Saches = new HashSet<Sach>();
            Taikhoans = new HashSet<Taikhoan>();
            Thongkes = new HashSet<Thongke>();
        }

        public string Magv { get; set; }
        public string Hoten { get; set; }
        public string Email { get; set; }
        public string Gioitinh { get; set; }
        public int Idkhoa { get; set; }

        public virtual KhoaBm IdkhoaNavigation { get; set; }
        public virtual ICollection<Sach> Saches { get; set; }
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
        public virtual ICollection<Thongke> Thongkes { get; set; }
    }
}
