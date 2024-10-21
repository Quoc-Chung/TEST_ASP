using _1._alazea_gh_pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Route("ThemSachMoi")]
        [HttpGet]
      
        public IActionResult ThemSachMoi()
        {
            ViewBag.MaLoai = new SelectList(db.TLoaiSaches.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNgonNgu = new SelectList(db.TNgonNgus.ToList(), "MaNgonNgu", "TenNgonNgu");
            ViewBag.MaNxb = new SelectList(db.TNhaXbs.ToList(), "MaNxb", "TenNxb");
            var list_Sach = db.TSaches.ToList();
            ViewBag.TenNgonNgu = db.TNgonNgus.ToList();
            return View();
        }

        [Route("ThemSachMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSachMoi(TSach Sach)
        {
            /*- == true thì chứng tỏ dứ liệu truyền từ form vào hợp lệ -*/
            if (ModelState.IsValid)
            {
                db.Add(Sach);
                db.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách
            }
            var list_Sach = db.TSaches.ToList();
            ViewBag.TenNgonNgu = db.TNgonNgus.ToList();
            return View();
        }






        /*- Xóa sách thì chúng ta sẽ phải cần mã sách -*/
        [Route("XoaSach")]
        [HttpGet]
        public IActionResult XoaSach(string MaSach)
        {
            TempData["Message"] = "";
            var list_Sach = db.TSaches.ToList();
            ViewBag.TenNgonNgu = db.TNgonNgus.ToList();
            // Kiểm tra chi tiết sản phẩm trước khi xóa
            var chiTietSachTonTai = db.TBanSaoSaches.Any(x => x.MaSach == MaSach);
            if (chiTietSachTonTai)
            {
                TempData["Message"] = "Không xóa được sản phẩm này vì có chi tiết sản phẩm liên quan.";
                return RedirectToAction("Index", "Home");
            }

            // Xóa sản phẩm trong bảng TDanhMucSps
            var Sach = db.TSaches.Find(MaSach);
            if (Sach != null)
            {
                db.Remove(Sach);
                db.SaveChanges();
                TempData["Message"] = "Sản phẩm đã được xóa.";
            }
            else
            {
                TempData["Message"] = "Sản phẩm không tồn tại.";
            }

            return RedirectToAction("Index", "Home");
        }



    }
}
