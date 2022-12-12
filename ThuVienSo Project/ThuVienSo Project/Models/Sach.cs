using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Sach
    {
        public Sach()
        {
            Thongkes = new HashSet<Thongke>();
        }

        public int Masach { get; set; }
        public int Madanhmuc { get; set; }
        public int Idmon { get; set; }
        public string Tacgia { get; set; }
        public string Tensach { get; set; }
        public string Anh { get; set; }
        public string Filedata { get; set; }
        public string Gioithieu { get; set; }
        public int? Luottai { get; set; }
        public int? Luotxem { get; set; }
        public double? Diemdanhgia { get; set; }
        public int? Luotdanhgia { get; set; }
        public string Magv { get; set; }
        public DateTime Ngaydang { get; set; }
        public string Keyword { get; set; }

        public virtual Monhoc IdmonNavigation { get; set; }
        public virtual Danhmuc MadanhmucNavigation { get; set; }
        public virtual Giangvien MagvNavigation { get; set; }
        public virtual ICollection<Thongke> Thongkes { get; set; }
    }
}
