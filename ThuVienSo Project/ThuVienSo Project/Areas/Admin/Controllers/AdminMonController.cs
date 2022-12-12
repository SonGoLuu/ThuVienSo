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
    public class AdminMonController : Controller
    {
        private readonly thuviensoContext _context;
        public INotyfService _notifyService { get; }
        public AdminMonController(thuviensoContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/AdminMon
        public IActionResult Index(int? page)
        {
            
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsMon = _context.Monhocs.AsNoTracking()
                .Include(x => x.ManganhNavigation)
                .OrderByDescending(x => x.Idmon);

            PagedList<Monhoc> models = new PagedList<Monhoc>(lsMon, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewData["Manganh"] = new SelectList(_context.Nganhhocs, "Manganh", "Tennganh");
            return View(models);
        }

        // GET: Admin/AdminMon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monhoc = await _context.Monhocs
                .Include(m => m.ManganhNavigation)
                .FirstOrDefaultAsync(m => m.Idmon == id);
            if (monhoc == null)
            {
                return NotFound();
            }

            return View(monhoc);
        }

        // GET: Admin/AdminMon/Create
        public IActionResult Create()
        {
            ViewData["Manganh"] = new SelectList(_context.Nganhhocs, "Manganh", "Tennganh");
            return View();
        }

        // POST: Admin/AdminMon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idmon,Tenmon,Manganh")] Monhoc monhoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monhoc);
                await _context.SaveChangesAsync();
                _notifyService.Success("Add Success");
                return RedirectToAction(nameof(Index));
            }
            ViewData["Manganh"] = new SelectList(_context.Nganhhocs, "Manganh", "Tennganh", monhoc.Manganh);
            return View(monhoc);
        }

        // GET: Admin/AdminMon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monhoc = await _context.Monhocs.FindAsync(id);
            if (monhoc == null)
            {
                return NotFound();
            }
            ViewData["Manganh"] = new SelectList(_context.Nganhhocs, "Manganh", "Tennganh", monhoc.Manganh);
            return View(monhoc);
        }

        // POST: Admin/AdminMon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idmon,Tenmon,Manganh")] Monhoc monhoc)
        {
            if (id != monhoc.Idmon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monhoc);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonhocExists(monhoc.Idmon))
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
            ViewData["Manganh"] = new SelectList(_context.Nganhhocs, "Manganh", "Tennganh", monhoc.Manganh);
            return View(monhoc);
        }

        // GET: Admin/AdminMon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monhoc = await _context.Monhocs
                .Include(m => m.ManganhNavigation)
                .FirstOrDefaultAsync(m => m.Idmon == id);
            if (monhoc == null)
            {
                return NotFound();
            }

            return View(monhoc);
        }

        // POST: Admin/AdminMon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monhoc = await _context.Monhocs.FindAsync(id);
            _context.Monhocs.Remove(monhoc);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete Success");
            return RedirectToAction(nameof(Index));
        }

        private bool MonhocExists(int id)
        {
            return _context.Monhocs.Any(e => e.Idmon == id);
        }
    }
}
