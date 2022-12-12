using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSinhvienController : Controller
    {
        private readonly thuviensoContext _context;
        public INotyfService _notifyService { get; }
        public AdminSinhvienController(thuviensoContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminSinhvien
        public IActionResult Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsSV = _context.Sinhviens.AsNoTracking()
                .Include(x => x.IdkhoaNavigation)
                .OrderByDescending(x => x.Masinhvien);

            PagedList<Sinhvien> models = new PagedList<Sinhvien>(lsSV, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa");
            return View(models);
        }

        // GET: Admin/AdminSinhvien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhviens
                .Include(s => s.IdkhoaNavigation)
                .FirstOrDefaultAsync(m => m.Masinhvien == id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            return View(sinhvien);
        }

        // GET: Admin/AdminSinhvien/Create
        public IActionResult Create()
        {
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa");
            return View();
        }

        // POST: Admin/AdminSinhvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masinhvien,Idkhoa,Hoten,Lop,Gioitinh")] Sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhvien);
                await _context.SaveChangesAsync();
                _notifyService.Success("Add Success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa", sinhvien.Idkhoa);
            return View(sinhvien);
        }

        // GET: Admin/AdminSinhvien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound();
            }
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa", sinhvien.Idkhoa);
            return View(sinhvien);
        }

        // POST: Admin/AdminSinhvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Masinhvien,Idkhoa,Hoten,Lop,Gioitinh")] Sinhvien sinhvien)
        {
            if (id != sinhvien.Masinhvien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhvien);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhvienExists(sinhvien.Masinhvien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa", sinhvien.Idkhoa);
            return View(sinhvien);
        }

        // GET: Admin/AdminSinhvien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhviens
                .Include(s => s.IdkhoaNavigation)
                .FirstOrDefaultAsync(m => m.Masinhvien == id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            return View(sinhvien);
        }

        // POST: Admin/AdminSinhvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhvien = await _context.Sinhviens.FindAsync(id);
            _context.Sinhviens.Remove(sinhvien);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete Success");
            return RedirectToAction(nameof(Index));
        }

        private bool SinhvienExists(string id)
        {
            return _context.Sinhviens.Any(e => e.Masinhvien == id);
        }
    }
}
