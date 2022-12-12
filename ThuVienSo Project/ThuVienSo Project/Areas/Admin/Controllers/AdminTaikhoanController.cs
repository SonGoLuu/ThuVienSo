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
    public class AdminTaikhoanController : Controller
    {
        private readonly thuviensoContext _context;
        public INotyfService _notifyService { get; }
        public AdminTaikhoanController(thuviensoContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminTaikhoan
        public IActionResult Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTK = _context.Taikhoans.AsNoTracking()
                .Include(x => x.MagvNavigation)
                .Include(x => x.MasinhvienNavigation)
                .OrderByDescending(x => x.Masinhvien);

            PagedList<Taikhoan> models = new PagedList<Taikhoan>(lsTK, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv");
            ViewData["Masinhvien"] = new SelectList(_context.Sinhviens, "Masinhvien", "Masinhvien");
            return View(models);
        }

        // GET: Admin/AdminTaikhoan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .Include(t => t.MagvNavigation)
                .Include(t => t.MasinhvienNavigation)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoan/Create
        public IActionResult Create()
        {
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv");
            ViewData["Masinhvien"] = new SelectList(_context.Sinhviens, "Masinhvien", "Masinhvien");
            return View();
        }

        // POST: Admin/AdminTaikhoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idaccount,Username,Passwords,Loaiaccount,Magv,Masinhvien")] Taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                _notifyService.Success("Add Success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", taikhoan.Magv);
            ViewData["Masinhvien"] = new SelectList(_context.Sinhviens, "Masinhvien", "Masinhvien", taikhoan.Masinhvien);
            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound();
            }
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", taikhoan.Magv);
            ViewData["Masinhvien"] = new SelectList(_context.Sinhviens, "Masinhvien", "Masinhvien", taikhoan.Masinhvien);
            return View(taikhoan);
        }

        // POST: Admin/AdminTaikhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Passwords,Loaiaccount,Magv,Masinhvien")] Taikhoan taikhoan)
        {
            if (id != taikhoan.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taikhoan);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikhoanExists(taikhoan.Username))
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
            ViewData["Magv"] = new SelectList(_context.Giangviens, "Magv", "Magv", taikhoan.Magv);
            ViewData["Masinhvien"] = new SelectList(_context.Sinhviens, "Masinhvien", "Masinhvien", taikhoan.Masinhvien);
            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .Include(t => t.MagvNavigation)
                .Include(t => t.MasinhvienNavigation)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // POST: Admin/AdminTaikhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            _context.Taikhoans.Remove(taikhoan);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete Success");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Xemtailieu()
        {
            return View();
        }
        private bool TaikhoanExists(string id)
        {
            return _context.Taikhoans.Any(e => e.Username == id);
        }
    }
}
