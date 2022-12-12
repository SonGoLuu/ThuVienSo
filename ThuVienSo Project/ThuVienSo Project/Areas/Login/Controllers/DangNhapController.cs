using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThuVienSo_Project.Models;

namespace ThuVienSo_Project.Areas.Login.Controllers
{
    [Area("Login")]
    public class DangNhapController : Controller
    {
        private readonly thuviensoContext _context;
        public DangNhapController(thuviensoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Getuserpass(string user, string pass)
        {
            List<Taikhoan> lsTaiKhoan = new List<Taikhoan>();
            lsTaiKhoan = _context.Taikhoans.AsNoTracking()
                .Include(x => x.MasinhvienNavigation)
                .Include(x => x.MagvNavigation)
                .OrderByDescending(x => x.Username).ToList();
            int i = 0; int y = -1;
            foreach(var item in lsTaiKhoan)
            {
                if(item.Username == user)
                {
                    if (item.Passwords == pass)
                    {
                        i = 2;
                        if (item.Loaiaccount == 0) y = 0;
                        else if (item.Loaiaccount == 1) y = 1;
                        else y = 2;
                    }
                    else i = 1;
                }    
            }
            if (i == 0) return Json(new { status = "sai tai khoan" });
            else if (i == 1) return Json(new { status = "sai mat khau" });
            else
            { 
                if(y==0) return Json(new { status = "admin" });
                else if (y == 1) return Json(new { status = "giangvien" });
                else return Json(new { status = "sinhvien" });
            }
        }
    }
}
