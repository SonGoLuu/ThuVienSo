using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Danhmuc
    {
        public Danhmuc()
        {
            Saches = new HashSet<Sach>();
        }

        public int Madanhmuc { get; set; }
        public string Tendanhmuc { get; set; }

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
