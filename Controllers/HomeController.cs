using _1._alazea_gh_pages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _1._alazea_gh_pages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        QlthuVienContext db = new QlthuVienContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list_Sach = db.TSaches.ToList();
            ViewBag.TenNgonNgu = db.TNgonNgus.ToList();

            return View(list_Sach);
        }

        public IActionResult Privacy()
        {
            return View();
        }
     
    }
}
