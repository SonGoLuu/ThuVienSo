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
    public class AdminGiangvienController : Controller
    {
        private readonly thuviensoContext _context;
        public INotyfService _notifyService { get; }
        public AdminGiangvienController(thuviensoContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminGiangvien
        public IActionResult Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsGV = _context.Giangviens.AsNoTracking()
                .Include(x => x.IdkhoaNavigation)
                .OrderByDescending(x => x.Magv);

            PagedList<Giangvien> models = new PagedList<Giangvien>(lsGV, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa");
            return View(models);
        }

        // GET: Admin/AdminGiangvien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangvien = await _context.Giangviens
                .Include(g => g.IdkhoaNavigation)
                .FirstOrDefaultAsync(m => m.Magv == id);
            if (giangvien == null)
            {
                return NotFound();
            }

            return View(giangvien);
        }

        // GET: Admin/AdminGiangvien/Create
        public IActionResult Create()
        {
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa");
            return View();
        }

        // POST: Admin/AdminGiangvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Magv,Hoten,Email,Gioitinh,Idkhoa")] Giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giangvien);
                await _context.SaveChangesAsync();
                _notifyService.Success("Add Success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa", giangvien.Idkhoa);
            
            return View(giangvien);
        }

        // GET: Admin/AdminGiangvien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangvien = await _context.Giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return NotFound();
            }
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa", giangvien.Idkhoa);
            return View(giangvien);
        }

        // POST: Admin/AdminGiangvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Magv,Hoten,Email,Gioitinh,Idkhoa")] Giangvien giangvien)
        {
            if (id != giangvien.Magv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giangvien);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiangvienExists(giangvien.Magv))
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
            ViewData["Idkhoa"] = new SelectList(_context.KhoaBms, "Idkhoa", "Tenkhoa", giangvien.Idkhoa);
            return View(giangvien);
        }

        // GET: Admin/AdminGiangvien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giangvien = await _context.Giangviens
                .Include(g => g.IdkhoaNavigation)
                .FirstOrDefaultAsync(m => m.Magv == id);
            if (giangvien == null)
            {
                return NotFound();
            }

            return View(giangvien);
        }

        // POST: Admin/AdminGiangvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var giangvien = await _context.Giangviens.FindAsync(id);
            _context.Giangviens.Remove(giangvien);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete Success");
            return RedirectToAction(nameof(Index));
        }

        private bool GiangvienExists(string id)
        {
            return _context.Giangviens.Any(e => e.Magv == id);
        }
    }
}
