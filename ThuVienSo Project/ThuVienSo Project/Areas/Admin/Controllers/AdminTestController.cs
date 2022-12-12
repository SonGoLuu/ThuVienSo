using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Collections.Generic;
using System.Linq;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTestController : Controller
    {
        private readonly thuviensoContext _context;
        public AdminTestController(thuviensoContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTest
        public IActionResult Index(int page = 1, int Idmon = 0)
        {
            var pageNumber = page;
            var pageSize = 10;
            List<Sach> lsBooks = new List<Sach>();
            if (Idmon != 0)
            {
                lsBooks = _context.Saches.AsNoTracking()
                    .Where(x => x.Idmon == Idmon)
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
            ViewBag.CurrentCateID = Idmon;
            ViewBag.CurrentPage = pageNumber;

            ViewData["Idmon"] = new SelectList(_context.Monhocs, "Idmon", "Tenmon", Idmon);

            return View(models);
        }
        public IActionResult Filtter(int Idmon = 0)
        {
            var url = $"/Admin/AdminTest?Idmon={Idmon}";
            if (Idmon == 0)
            {
                url = $"/Admin/AdminTest";
            }
            return Json(new { status = "success", redirectUrl = url });
        }
    }
}
