using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly thuviensoContext _context;

        public SearchController(thuviensoContext context)
        {
            _context = context;
        }

        // GET: Admin/Search/FindBooks
        [HttpPost]
        public IActionResult FindBooks(string keyword)
        {
            List<Sach> ls = new List<Sach>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListBooksSearchPartial", null);
            }
            ls = _context.Saches
                  .AsNoTracking()
                  .Include(a => a.MadanhmucNavigation)
                  .Where(x => x.Tensach.Contains(keyword))
                  .OrderByDescending(x => x.Tensach)
                  .Take(10)
                  .ToList();
            if (ls == null)
            {
                return PartialView("ListBooksSearchPartial", null);
            }
            else
            {
                return PartialView("ListBooksSearchPartial", ls);
            }
        }
    }
}
