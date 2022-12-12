using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Nganhhoc
    {
        public Nganhhoc()
        {
            Monhocs = new HashSet<Monhoc>();
        }

        public int Manganh { get; set; }
        public string Tennganh { get; set; }

        public virtual ICollection<Monhoc> Monhocs { get; set; }
    }
}
