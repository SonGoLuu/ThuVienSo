using Microsoft.AspNetCore.Mvc;

namespace ThuVienSo_Project.Areas.GiangVien.Controllers
{
    [Area("GiangVien")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
