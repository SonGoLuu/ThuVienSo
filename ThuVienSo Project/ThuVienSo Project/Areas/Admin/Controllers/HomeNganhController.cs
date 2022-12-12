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
    public class HomeNganhController : Controller
    {
        private readonly thuviensoContext _context;
        public INotyfService _notifyService { get; }
        public HomeNganhController(thuviensoContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: Admin/HomeNganh
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsNganh = _context.Nganhhocs.AsNoTracking()
                .OrderByDescending(x => x.Manganh);

            PagedList<Nganhhoc> models = new PagedList<Nganhhoc>(lsNganh, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/HomeNganh/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nganhhoc = await _context.Nganhhocs
                .FirstOrDefaultAsync(m => m.Manganh == id);
            if (nganhhoc == null)
            {
                return NotFound();
            }

            return View(nganhhoc);
        }

        // GET: Admin/HomeNganh/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HomeNganh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Manganh,Tennganh")] Nganhhoc nganhhoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nganhhoc);
                await _context.SaveChangesAsync();
                _notifyService.Success("Add Success");
                return RedirectToAction(nameof(Index));
            }
            return View(nganhhoc);
        }

        // GET: Admin/HomeNganh/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nganhhoc = await _context.Nganhhocs.FindAsync(id);
            if (nganhhoc == null)
            {
                return NotFound();
            }
            return View(nganhhoc);
        }

        // POST: Admin/HomeNganh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Manganh,Tennganh")] Nganhhoc nganhhoc)
        {
            if (id != nganhhoc.Manganh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nganhhoc);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NganhhocExists(nganhhoc.Manganh))
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
            return View(nganhhoc);
        }

        // GET: Admin/HomeNganh/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nganhhoc = await _context.Nganhhocs
                .FirstOrDefaultAsync(m => m.Manganh == id);
            if (nganhhoc == null)
            {
                return NotFound();
            }

            return View(nganhhoc);
        }

        // POST: Admin/HomeNganh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nganhhoc = await _context.Nganhhocs.FindAsync(id);
            _context.Nganhhocs.Remove(nganhhoc);
            await _context.SaveChangesAsync();
            _notifyService.Success("Delete Success");
            return RedirectToAction(nameof(Index));
        }

        private bool NganhhocExists(int id)
        {
            return _context.Nganhhocs.Any(e => e.Manganh == id);
        }
    }
}
