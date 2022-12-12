using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Controllers
{
    public class SearchController : Controller
    {
        private readonly thuviensoContext _context;

        public SearchController(thuviensoContext context)
        {
            _context = context;
        }

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

