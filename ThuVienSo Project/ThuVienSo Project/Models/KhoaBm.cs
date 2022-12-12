using System;
using System.Collections.Generic;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class KhoaBm
    {
        public KhoaBm()
        {
            Giangviens = new HashSet<Giangvien>();
            Sinhviens = new HashSet<Sinhvien>();
        }

        public int Idkhoa { get; set; }
        public string Tenkhoa { get; set; }

        public virtual ICollection<Giangvien> Giangviens { get; set; }
        public virtual ICollection<Sinhvien> Sinhviens { get; set; }
    }
}
