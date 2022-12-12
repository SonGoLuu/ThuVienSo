using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ThuVienSo_Project.Models
{
    public partial class thuviensoContext : DbContext
    {
        public thuviensoContext()
        {
        }

        public thuviensoContext(DbContextOptions<thuviensoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Danhmuc> Danhmucs { get; set; }
        public virtual DbSet<Giangvien> Giangviens { get; set; }
        public virtual DbSet<KhoaBm> KhoaBms { get; set; }
        public virtual DbSet<Monhoc> Monhocs { get; set; }
        public virtual DbSet<Nganhhoc> Nganhhocs { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<Sinhvien> Sinhviens { get; set; }
        public virtual DbSet<Taikhoan> Taikhoans { get; set; }
        public virtual DbSet<Thongke> Thongkes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-5LK6RF58;Initial Catalog=thuvienso;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Danhmuc>(entity =>
            {
                entity.HasKey(e => e.Madanhmuc)
                    .HasName("pk_danhmuc");

                entity.ToTable("danhmuc");

                entity.Property(e => e.Madanhmuc).HasColumnName("madanhmuc");

                entity.Property(e => e.Tendanhmuc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tendanhmuc");
            });

            modelBuilder.Entity<Giangvien>(entity =>
            {
                entity.HasKey(e => e.Magv)
                    .HasName("pk_giangvien");

                entity.ToTable("giangvien");

                entity.Property(e => e.Magv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("magv");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gioitinh)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("gioitinh")
                    .IsFixedLength(true);

                entity.Property(e => e.Hoten)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Idkhoa).HasColumnName("idkhoa");

                entity.HasOne(d => d.IdkhoaNavigation)
                    .WithMany(p => p.Giangviens)
                    .HasForeignKey(d => d.Idkhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_giangvien");
            });

            modelBuilder.Entity<KhoaBm>(entity =>
            {
                entity.HasKey(e => e.Idkhoa)
                    .HasName("pk_khoaBM");

                entity.ToTable("khoaBM");

                entity.Property(e => e.Idkhoa).HasColumnName("idkhoa");

                entity.Property(e => e.Tenkhoa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenkhoa");
            });

            modelBuilder.Entity<Monhoc>(entity =>
            {
                entity.HasKey(e => e.Idmon)
                    .HasName("pk_monhoc");

                entity.ToTable("monhoc");

                entity.Property(e => e.Idmon).HasColumnName("idmon");

                entity.Property(e => e.Manganh).HasColumnName("manganh");

                entity.Property(e => e.Tenmon)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenmon");

                entity.HasOne(d => d.ManganhNavigation)
                    .WithMany(p => p.Monhocs)
                    .HasForeignKey(d => d.Manganh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_monhoc");
            });

            modelBuilder.Entity<Nganhhoc>(entity =>
            {
                entity.HasKey(e => e.Manganh)
                    .HasName("pk_nganhhoc");

                entity.ToTable("nganhhoc");

                entity.Property(e => e.Manganh).HasColumnName("manganh");

                entity.Property(e => e.Tennganh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tennganh");
            });

            modelBuilder.Entity<Sach>(entity =>
            {
                entity.HasKey(e => e.Masach)
                    .HasName("pk_sach");

                entity.ToTable("sach");

                entity.Property(e => e.Masach).HasColumnName("masach");

                entity.Property(e => e.Anh).HasColumnName("anh");

                entity.Property(e => e.Diemdanhgia).HasColumnName("diemdanhgia");

                entity.Property(e => e.Filedata)
                    .IsRequired()
                    .HasColumnName("filedata");

                entity.Property(e => e.Gioithieu).HasColumnName("gioithieu");

                entity.Property(e => e.Idmon).HasColumnName("idmon");

                entity.Property(e => e.Keyword)
                    .HasMaxLength(50)
                    .HasColumnName("keyword");

                entity.Property(e => e.Luotdanhgia).HasColumnName("luotdanhgia");

                entity.Property(e => e.Luottai).HasColumnName("luottai");

                entity.Property(e => e.Luotxem).HasColumnName("luotxem");

                entity.Property(e => e.Madanhmuc).HasColumnName("madanhmuc");

                entity.Property(e => e.Magv)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("magv");

                entity.Property(e => e.Ngaydang)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaydang");

                entity.Property(e => e.Tacgia)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tacgia");

                entity.Property(e => e.Tensach)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tensach");

                entity.HasOne(d => d.IdmonNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.Idmon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sach_monhoc");

                entity.HasOne(d => d.MadanhmucNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.Madanhmuc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sach_danhmuc");

                entity.HasOne(d => d.MagvNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.Magv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sach_giangvien");
            });

            modelBuilder.Entity<Sinhvien>(entity =>
            {
                entity.HasKey(e => e.Masinhvien)
                    .HasName("pk_sinhvien");

                entity.ToTable("sinhvien");

                entity.Property(e => e.Masinhvien)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("masinhvien");

                entity.Property(e => e.Gioitinh)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("gioitinh")
                    .IsFixedLength(true);

                entity.Property(e => e.Hoten)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Idkhoa).HasColumnName("idkhoa");

                entity.Property(e => e.Lop)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lop");

                entity.HasOne(d => d.IdkhoaNavigation)
                    .WithMany(p => p.Sinhviens)
                    .HasForeignKey(d => d.Idkhoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sinhvien");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("pk_taikhoan");

                entity.ToTable("taikhoan");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Loaiaccount).HasColumnName("loaiaccount");

                entity.Property(e => e.Magv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("magv");

                entity.Property(e => e.Masinhvien)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("masinhvien");

                entity.Property(e => e.Passwords)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("passwords");

                entity.HasOne(d => d.MagvNavigation)
                    .WithMany(p => p.Taikhoans)
                    .HasForeignKey(d => d.Magv)
                    .HasConstraintName("fk_taikhoan_giangvien");

                entity.HasOne(d => d.MasinhvienNavigation)
                    .WithMany(p => p.Taikhoans)
                    .HasForeignKey(d => d.Masinhvien)
                    .HasConstraintName("fk_taikhoan_sinhvien");
            });

            modelBuilder.Entity<Thongke>(entity =>
            {
                entity.HasKey(e => e.Idthongke)
                    .HasName("pk_thongke");

                entity.ToTable("thongke");

                entity.Property(e => e.Idthongke).HasColumnName("idthongke");

                entity.Property(e => e.Magv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("magv");

                entity.Property(e => e.Masach).HasColumnName("masach");

                entity.Property(e => e.Masinhvien)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("masinhvien");

                entity.Property(e => e.Ngaydoc)
                    .HasColumnType("date")
                    .HasColumnName("ngaydoc");

                entity.Property(e => e.Ngaytai)
                    .HasColumnType("date")
                    .HasColumnName("ngaytai");

                entity.HasOne(d => d.MagvNavigation)
                    .WithMany(p => p.Thongkes)
                    .HasForeignKey(d => d.Magv)
                    .HasConstraintName("fk_thongke_giangvien");

                entity.HasOne(d => d.MasachNavigation)
                    .WithMany(p => p.Thongkes)
                    .HasForeignKey(d => d.Masach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_thongke_sach");

                entity.HasOne(d => d.MasinhvienNavigation)
                    .WithMany(p => p.Thongkes)
                    .HasForeignKey(d => d.Masinhvien)
                    .HasConstraintName("fk_thongke_sinhvien");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
