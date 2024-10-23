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


        [Route("SuaSach")]
        /*- Tạo một cái trang đưa dữ liệu lên - */
        [HttpGet]
        public IActionResult SuaSach(String masach)
        {

            ViewBag.MaLoai = new SelectList(db.TLoaiSaches.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNgonNgu = new SelectList(db.TNgonNgus.ToList(), "MaNgonNgu", "TenNgonNgu");
            ViewBag.MaNxb = new SelectList(db.TNhaXbs.ToList(), "MaNxb", "TenNxb");
            var Sach = db.TSaches.Find(masach);
            /*- HIỆN NÓ LÊN BẰNG CÁCH RETURN VỀ VIEW SẢN PHẨM -*/
            ViewBag.TenNgonNgu = db.TNgonNgus.ToList();
            return View(Sach);
        }
       [Route("SuaSach")]
      /*- Tạo một cái trang đưa dữ liệu lên - */
       [HttpPost]
        /* - Kiểm tra dữ liệu nhập vào có chính xác với quy định về validation không  -*/
        [ValidateAntiForgeryToken]
        public IActionResult SuaSach(TSach Sach)
        {
            if (ModelState.IsValid)
            {
                db.Update(Sach);
                db.SaveChanges();
                /* - SAU KHI TẠO MỚI XONG SẼ CHUYỂN SANG DANH MụC SẢN PHẨM -*/
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TenNgonNgu = db.TNgonNgus.ToList();
            /* - NẾU NHẬP KHÔNG CHÍNH XÁC THÌ CÓ THỂ NHẬP LẠI ĐỂ TẠO CÁI VIEW CHO HỌ CÓ THỂ NHẬP LẠI -*/
            return View(Sach);
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

        public IActionResult ChiTietSanPham(string MaSach)
        {
            /*- TRÍCH XUẤT RA CÁI MÃ SẢN PHẨM - */
            var SanPham = db.TSaches.SingleOrDefault(x => x.MaSach == MaSach);

            return View(SanPham);
        }
    }
}
