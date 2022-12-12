using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class Monhoc
    {
        public Monhoc()
        {
            Saches = new HashSet<Sach>();
        }

        public int Idmon { get; set; }
        public string Tenmon { get; set; }
        public int Manganh { get; set; }

        public virtual Nganhhoc ManganhNavigation { get; set; }
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
