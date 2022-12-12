using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThuVienSo_Project.Helpper;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Controllers
{

    public class TailieuController : Controller
    {
        private readonly thuviensoContext _context;
        public TailieuController(thuviensoContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int Idmon = 0, int Madanhmuc = 0, int Manganh = 0, string Keyword = "", int idlayout=0)
        {
            ViewBag.idlayout = idlayout;
            var pageNumber = page;
            var pageSize = 7;
            List<Sach> lsBooks = new List<Sach>();
            if (Idmon != 0)
            {
                lsBooks = _context.Saches.AsNoTracking()
                    .Where(x => x.Idmon == Idmon)
                .Include(x => x.MadanhmucNavigation)
                .Include(x => x.IdmonNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Masach).ToList();
                ViewBag.CurrentCateID = Idmon;
            }
            else if (Madanhmuc != 0)
            {
                lsBooks = _context.Saches.AsNoTracking()
                    .Where(x => x.Madanhmuc == Madanhmuc)
                .Include(x => x.MadanhmucNavigation)
                .Include(x => x.IdmonNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Masach).ToList();
                ViewBag.CurrentCateID = Madanhmuc;
            }
            else if (Manganh != 0)
            {
                lsBooks = _context.Saches.AsNoTracking()
                    .Where(x => x.IdmonNavigation.Manganh == Manganh)
                .Include(x => x.MadanhmucNavigation)
                .Include(x => x.IdmonNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Masach).ToList();
                ViewBag.CurrentCateID = Manganh;
            }
            else if (String.Compare(Keyword, "", true) != 0)
            {
                lsBooks = _context.Saches.AsNoTracking()
                    .Where(x => x.Keyword.Contains(Keyword) || x.Tensach.Contains(Keyword))
                .Include(x => x.MadanhmucNavigation)
                .Include(x => x.IdmonNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Masach).ToList();
                ViewBag.CurrentCateID = Keyword;
            }
            else
            {
                lsBooks = _context.Saches.AsNoTracking()
                .Include(x => x.MadanhmucNavigation)
                .Include(x => x.IdmonNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Masach).ToList();
                ViewBag.CurrentCateID = Idmon;
            }
            PagedList<Sach> models = new PagedList<Sach>(lsBooks.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;

            ViewData["Idmon"] = new SelectList(_context.Monhocs, "Idmon", "Tenmon", Idmon);
            ViewData["Madanhmuc"] = new SelectList(_context.Danhmucs, "Madanhmuc", "Tendanhmuc", Madanhmuc);
            ViewData["Manganh"] = new SelectList(_context.Danhmucs, "Manganh", "Tennganh", Manganh);
            return View(models);
        }
        public IActionResult Filtter(int Idmon = 0, int idlayout=0)
        {
            var url = $"/Tailieu?Idmon={Idmon}&idlayout={idlayout}";
            if (Idmon == 0)
            {
                url = $"/Tailieu?idlayout={idlayout}";
            }
            return Json(new { status = "success", redirectUrl = url });
        }
        public async Task<IActionResult> Details(int? id, int idlayout)
        {
            ViewBag.idlayout = idlayout;
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.IdmonNavigation)
                .Include(s => s.MadanhmucNavigation)
                .Include(s => s.MagvNavigation)
                .FirstOrDefaultAsync(m => m.Masach == id);
            
            ViewBag.Context = _context;
            ViewBag.Diemdanhgia = sach.Diemdanhgia;
            ViewBag.Luotdanhgia = sach.Luotdanhgia;
            ViewBag.Capnhatdiem = 0;
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }
        public string GetDataPath(string file) => $"wwwroot\\tailieu\\filetailieu\\{file}";
        public (Stream, string) Taixuong(Sach b)
        {
            var memmory = new MemoryStream();
            using var stream = new FileStream(GetDataPath(b.Filedata), FileMode.Open);
            stream.CopyTo(memmory);
            memmory.Position = 0;
            var type = Path.GetExtension(b.Filedata) switch
            {
                "pdf" => "application/pdf",
                "docx" => "application/vnd.ms-word",
                "doc" => "application/vnd.ms-word",
                "txt" => "text/plain",
                _ => "application/pdf"
            };
            return (memmory, type);
        }
        public async Task<IActionResult> Read(int id, int idlayout)
        {
            var b = await _context.Saches
             .Include(s => s.IdmonNavigation)
             .Include(s => s.MadanhmucNavigation)
             .Include(s => s.MagvNavigation)
             .FirstOrDefaultAsync(m => m.Masach == id);
            if (b == null) return NotFound();
            if (!System.IO.File.Exists(GetDataPath(b.Filedata))) return NotFound();
            b.Luottai = b.Luottai + 1;
            _context.Update(b);
            await _context.SaveChangesAsync();
            var (stream, type) = Taixuong(b);
            if (idlayout == 2) return File(stream, type, b.Filedata);
            else return Redirect("/Login?#");
            //return RedirectToAction("Details", "Tailieu", new { id = id });
        }

        [HttpPost]
        public async Task<JsonResult> SaveDanhGia(int id, double diem)
        {
            var b = await _context.Saches
                .Include(s => s.IdmonNavigation)
                .Include(s => s.MadanhmucNavigation)
                .Include(s => s.MagvNavigation)
                .FirstOrDefaultAsync(m => m.Masach == id);
            b.Diemdanhgia = diem;
            b.Luotdanhgia = b.Luotdanhgia + 1;
            _context.Update(b);
            int saveResult = await _context.SaveChangesAsync();
            if (saveResult > 0)
            {
                return Json(new { status = "ok" });
            }
            else
            {
                return Json(new { status = "err" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> LoadTopRateDoc()
        {
            List<Sach> b = await _context.Saches
                .OrderByDescending(x => x.Diemdanhgia)
                .Take(3).ToListAsync();

            return Json(new { status = "ok", sachs = b });
        }
    }
}
