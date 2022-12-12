using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public int checklog = 0;
        private readonly thuviensoContext _context;
        public HomeController(ILogger<HomeController> logger, thuviensoContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TVS()
        {
            return View();
        }

        public IActionResult News(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult Educate(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult About(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult Contact(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<JsonResult> LoadTopRateDoc()
        {
            List<Sach> b = await _context.Saches
                .OrderByDescending(x => x.Diemdanhgia)
                .Take(4).ToListAsync();

            return Json(new { status = "ok", sachs = b });
        }
    }
}
