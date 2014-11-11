using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.ServiceReference1;

namespace WebApp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        Service1Client service = new Service1Client();
        int pagesize = 12;
        public ActionResult Default(int page=1)
        {
            
           
            ViewBag.TotalPages = Math.Ceiling((double)service.GetData().Count / pagesize);
            return View(service.GetData().Skip((page - 1) * pagesize).Take(pagesize));
        }
         [ChildActionOnly]
        public ActionResult Menu()
        {


            return PartialView(service.Chude());
        }

        [ChildActionOnly]
        public ActionResult sanphambanchay()
        {

            return PartialView(service.Sanphambanchay());

        }
        [ChildActionOnly]
        public ActionResult DSLoaiTimKiem()
        {

            return PartialView(service.DSLoaiTimKiem());
        }
        public ActionResult Chitietsanpham()
        {
            string masp = Request.QueryString["masp"];
            int a = Convert.ToInt32(masp);
            Sach sach=service.Chitietsanpham(a);
            if (sach == null) return HttpNotFound();
            return View(sach);
        }

        [HttpPost]
        public ActionResult Default(FormCollection f)
        {
            return RedirectToAction("TimKiem", new { id = f["chuoitk"] });
        }
        public ActionResult Timkiem(string id, int page = 1)
        {
            var product = (from p in service.GetData() select p).ToList();

            ViewBag.Tensp = id;
            id = id.ToLower();
            if (!String.IsNullOrEmpty(id))
            {
                product = product.Where(a => a.TenSach.ToLower().Contains(id) && a.Active == null).ToList();

            }
            ViewBag.KhongTimThay = "Không tìm thấy các sản phẩm thỏa điều kiện!";
            ViewBag.TotalPages = Math.Ceiling((double)product.Count / pagesize);
            return View(product.Skip((page - 1) * pagesize).Take(pagesize));

        }

        int spcungloai = 8;
        [ChildActionOnly]
        //public ActionResult SPCungLoai(int page = 1)
        //{
        //    string masp = Request.QueryString["masp"];
        //    int a = Convert.ToInt32(masp);
        //    Sach sp = service.SPCungLoai1(a);
        //    int chude = Convert.ToInt32(sp.ChuDeID);

        //    var sanphamcungloai = service.SPCungLoai2(chude, a).ToList();
        //    ViewBag.masp = masp;
        //    ViewBag.TotalPages = Math.Ceiling((double)sanphamcungloai.Count / spcungloai);
        //    return PartialView(sanphamcungloai.Skip((page - 1) * spcungloai).Take(spcungloai));

        //}

        public ActionResult Danhmuc(int page = 1)
        {
            string chude = Request.QueryString["chude"];
            int a = Convert.ToInt32(chude);

            ViewBag.TotalPages = Math.Ceiling((double)service.TimkiemSachTheoChude(a).ToList().Count / 12);
            ViewBag.ChuDeID = chude;
            return View(service.TimkiemSachTheoChude(a).ToList().Skip((page - 1) * 12).Take(12));
        }

    }
}