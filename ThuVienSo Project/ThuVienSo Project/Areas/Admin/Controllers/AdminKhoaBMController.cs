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
    public class AdminKhoaBMController : Controller
    {
        private readonly thuviensoContext _context;
        public INotyfService _notifyService { get; }
        public AdminKhoaBMController(thuviensoContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminKhoaBM
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsKhoa = _context.KhoaBms.AsNoTracking()
                .OrderByDescending(x => x.Idkhoa);

            PagedList<KhoaBm> models = new PagedList<KhoaBm>(lsKhoa, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminKhoaBM/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaBm = await _context.KhoaBms
                .FirstOrDefaultAsync(m => m.Idkhoa == id);
            if (khoaBm == null)
            {
                return NotFound();
            }

            return View(khoaBm);
        }

        // GET: Admin/AdminKhoaBM/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminKhoaBM/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idkhoa,Tenkhoa")] KhoaBm khoaBm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khoaBm);
                await _context.SaveChangesAsync();
                _notifyService.Success("Add Success");
                return RedirectToAction(nameof(Index));
            }
            return View(khoaBm);
        }

        // GET: Admin/AdminKhoaBM/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaBm = await _context.KhoaBms.FindAsync(id);
            if (khoaBm == null)
            {
                return NotFound();
            }
            return View(khoaBm);
        }

        // POST: Admin/AdminKhoaBM/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idkhoa,Tenkhoa")] KhoaBm khoaBm)
        {
            if (id != khoaBm.Idkhoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoaBm);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaBmExists(khoaBm.Idkhoa))
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
            return View(khoaBm);
        }

        // GET: Admin/AdminKhoaBM/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaBm = await _context.KhoaBms
                .FirstOrDefaultAsync(m => m.Idkhoa == id);
            if (khoaBm == null)
            {
                return NotFound();
            }

            return View(khoaBm);
        }

        // POST: Admin/AdminKhoaBM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaBm = await _context.KhoaBms.FindAsync(id);
            _context.KhoaBms.Remove(khoaBm);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete Success");
            return RedirectToAction(nameof(Index));
        }

        private bool KhoaBmExists(int id)
        {
            return _context.KhoaBms.Any(e => e.Idkhoa == id);
        }
    }
}
