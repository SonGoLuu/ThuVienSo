using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ThuVienSo_Project.Helpper;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Areas.GiangVien.Controllers
{
    [Area("GiangVien")]
    public class UploadTaiLieuController : Controller
    {
        private readonly thuviensoContext _context;
        public INotyfService _notifyService { get; }
        public UploadTaiLieuController(thuviensoContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: GiangVien/UploadTaiLieu
        public IActionResult Index(int page = 1, int CatID = 0)
        {
            var pageNumber = page;
            var pageSize = 10;
            List<Sach> lsBooks = new List<Sach>();
            if (CatID != 0)
            {
                lsBooks = _context.Saches.AsNoTracking()
                    .Where(x => x.Madanhmuc == CatID)
                .Include(x => x.MadanhmucNavigation)
                .Include(x => x.IdmonNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Masach).ToList();
            }
            else
            {
                lsBooks = _context.Saches.AsNoTracking()
                .Include(x => x.MadanhmucNavigation)
                .Include(x => x.IdmonNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Masach).ToList();
            }
            PagedList<Sach> models = new PagedList<Sach>(lsBooks.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCateID = CatID;
            ViewBag.CurrentPage = pageNumber;

            ViewData["Madanhmuc"] = new SelectList(_context.Danhmucs, "Madanhmuc", "Tendanhmuc", CatID);

            return View(models);
        }

        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/GiangVien/UploadTaiLieu?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/GiangVien/UploadTaiLieu";
            }
            return Json(new { status = "success", redirectUrl = url });
        }
        // GET: GiangVien/UploadTaiLieu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.IdmonNavigation)
                .Include(s => s.MadanhmucNavigation)
                .Include(s => s.MagvNavigation)
                .FirstOrDefaultAsync(m => m.Masach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: GiangVien/UploadTaiLieu/Create
        public IActionResult Create()
        {
            ViewData["Idmon"] = new SelectList(_context.Monhocs, "Idmon", "Tenmon");
            ViewData["Madanhmuc"] = new SelectList(_context.Danhmucs, "Madanhmuc", "Tendanhmuc");
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Hoten");
            return View();
        }

        // POST: GiangVien/UploadTaiLieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masach,Madanhmuc,Idmon,Tacgia,Tensach,Anh,Filedata,Gioithieu,Luottai,Luotxem,Diemdanhgia,Luotdanhgia,Magv,Ngaydang,Keyword")] Sach sach, Microsoft.AspNetCore.Http.IFormFile fThumb, Microsoft.AspNetCore.Http.IFormFile tailieu)
        {
            if (ModelState.IsValid)
            {
                sach.Tensach = Utilities.ToTitleCase(sach.Tensach);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(sach.Tensach) + extension;
                    sach.Anh = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                }
                if (string.IsNullOrEmpty(sach.Tensach)) sach.Tensach = "default.jpg";
                sach.Keyword = Utilities.SEOUrl(sach.Tensach);
                sach.Ngaydang = DateTime.Now;

                if (tailieu != null)
                {
                    string extension = Path.GetExtension(tailieu.FileName);
                    string doc = Utilities.SEOUrl(sach.Tensach) + extension;
                    sach.Filedata = await Utilities.UploadTaiLieu(tailieu, @"filetailieu", doc.ToLower());
                }
                if (string.IsNullOrEmpty(sach.Tensach)) sach.Tensach = "default.pdf";

                _context.Add(sach);
                await _context.SaveChangesAsync();
                _notifyService.Success("Add Success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmon"] = new SelectList(_context.Monhocs, "Idmon", "Tenmon", sach.Idmon);
            ViewData["Madanhmuc"] = new SelectList(_context.Danhmucs, "Madanhmuc", "Tendanhmuc", sach.Madanhmuc);
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", sach.Magv);
            return View(sach);
        }

        // GET: GiangVien/UploadTaiLieu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            ViewData["Idmon"] = new SelectList(_context.Monhocs, "Idmon", "Tenmon", sach.Idmon);
            ViewData["Madanhmuc"] = new SelectList(_context.Danhmucs, "Madanhmuc", "Tendanhmuc", sach.Madanhmuc);
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Hoten", sach.Magv);
            return View(sach);
        }

        // POST: GiangVien/UploadTaiLieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Masach,Madanhmuc,Idmon,Tacgia,Tensach,Anh,Filedata,Gioithieu,Luottai,Luotxem,Diemdanhgia,Luotdanhgia,Magv,Ngaydang,Keyword")] Sach sach, Microsoft.AspNetCore.Http.IFormFile fThumb, Microsoft.AspNetCore.Http.IFormFile tailieu)
        {
            if (id != sach.Masach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sach.Tensach = Utilities.ToTitleCase(sach.Tensach);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(sach.Tensach) + extension;
                        sach.Anh = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(sach.Tensach)) sach.Tensach = "default.jpg";
                    sach.Keyword = Utilities.SEOUrl(sach.Tensach);
                    sach.Ngaydang = DateTime.Now;

                    if (tailieu != null)
                    {
                        string extension = Path.GetExtension(tailieu.FileName);
                        string doc = Utilities.SEOUrl(sach.Tensach) + extension;
                        sach.Filedata = await Utilities.UploadTaiLieu(tailieu, @"filetailieu", doc.ToLower());
                    }
                    if (string.IsNullOrEmpty(sach.Tensach)) sach.Tensach = "default.pdf";

                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.Masach))
                    {
                        _notifyService.Success("Error 404");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmon"] = new SelectList(_context.Monhocs, "Idmon", "Tenmon", sach.Idmon);
            ViewData["Madanhmuc"] = new SelectList(_context.Danhmucs, "Madanhmuc", "Tendanhmuc", sach.Madanhmuc);
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", sach.Magv);
            return View(sach);
        }

        // GET: GiangVien/UploadTaiLieu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _notifyService.Success("Delete Failed");
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.IdmonNavigation)
                .Include(s => s.MadanhmucNavigation)
                .Include(s => s.MagvNavigation)
                .FirstOrDefaultAsync(m => m.Masach == id);
            if (sach == null)
            {
                _notifyService.Success("Delete Failed");
                return NotFound();
            }
            return View(sach);
        }

        // POST: GiangVien/UploadTaiLieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sach = await _context.Saches.FindAsync(id);
            _context.Saches.Remove(sach);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete Success");
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
            return _context.Saches.Any(e => e.Masach == id);
        }
    }
}
