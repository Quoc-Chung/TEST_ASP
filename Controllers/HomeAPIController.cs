using _1._alazea_gh_pages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1._alazea_gh_pages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAPIController : ControllerBase
    {
        public QlthuVienContext db = new QlthuVienContext();
        [HttpGet("{mangonngu}")]
        public IEnumerable<TSach> LaySachTheoMa(string mangonngu)
        {
            var list_sach = db.TSaches.Where(x => x.MaNgonNgu == mangonngu).ToList();
            return list_sach;
        }

        /*- THÊM , SỬA , XÓA  SẢN PHẨM BẰNG API -*/ 
        
    }
}
