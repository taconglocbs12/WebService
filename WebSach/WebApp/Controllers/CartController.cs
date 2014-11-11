using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using  WebApp.ServiceReference1;

namespace WebApp.Controllers
{

    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Cart()
        {
            return View();
        }

        Service1Client service = new Service1Client();

        public ActionResult giohangthem(FormCollection f, string masanpham, int soluong = 1)
        {
            if (Session["Account"] != null)
            {
                //string acc = HttpContext.User.Identity.Name;// chỗ này nó ko lấy được cái ...chạy thu di... đi
                string acc = Session["Account"].ToString();

                List<Sach> sp = service.GetData().ToList();
                if (f["CT"] != null)
                {
                    soluong = int.Parse(f["CT"]);
                }
                //foreach (var item in sp)
                //{

                //    if (item.SachID.ToString().Contains(masanpham))
                //        GetCart().AddItem(item.SachID.ToString(), item.TenSach, item.GiaBan, soluong, acc);

                //}
                foreach (var item in sp)
                {
                    if (item.SachID.ToString().Contains(masanpham))
                    {
                        GetCart().AddItem(item.SachID.ToString(), item.TenSach, float.Parse(item.GiaBan.ToString()), soluong, acc);


                    }

                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Default", "Default");
        }


        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }
        public RedirectToRouteResult capnhatsoluong(string masanpham, string soluong)
        {
            if (IsNumber(soluong))
            {
                if (int.Parse(soluong) < 10)
                {
                    GetCart().capnhatsoluong(masanpham, int.Parse(soluong));
                }
                else { GetCart().capnhatsoluong(masanpham, 1); }
            }
            else
            {
                GetCart().capnhatsoluong(masanpham, 1);
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult giohangxoa(string masanpham)
        {

            GetCart().Remove(masanpham);
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult xoahet()
        {
            GetCart().Clear();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult menugiohang()
        {
            if (Session["Account"] == null )
            {
                GetCart().Clear();
            }
            return PartialView(new CartIndexViewModel { Cart = GetCart() });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ViewResult Index()
        {
            return View(new CartIndexViewModel { Cart = GetCart() });
        }
        public ViewResult ConfirmCart()
        {
            return View(new CartIndexViewModel { Cart = GetCart() });
        }
        public ActionResult Payment(FormCollection f)
        {

            CartIndexViewModel cart = new CartIndexViewModel { Cart = GetCart() };
            DatHang hd = new DatHang();
            
            var khachhang = service.KiemtraTendangnhap(Session["Account"].ToString()).ToList();
            string dc = "";
            foreach (var kh in khachhang)
            {
                if (f["diadiem"] == null)
                {
                    dc = kh.DiaChi;
                }
                else
                {
                    dc = f["diadiem"];
                }

                hd.KhachHangID = kh.KhachHangID;
                hd.TenKhachHang = kh.HoTen;
            }
            hd.DiaDiem = dc;
            hd.NgayDatHang = DateTime.Now;
            hd.TriGia = Convert.ToDouble(Request.QueryString["tongtien"]);
            hd.DaGiao = false;
            service.ThemDonDathang(int.Parse(hd.KhachHangID.ToString()), hd.TenKhachHang, hd.DiaDiem, hd.NgayDatHang, Double.Parse(hd.TriGia.ToString()), hd.DaGiao);

           
            foreach (var item in cart.Cart.Lines)
            {
                DatHangCT cthd = new DatHangCT();

                cthd.DatHangID = hd.DatHangID;
                cthd.SachID = int.Parse(item.masp);
                cthd.SoLuong = Convert.ToInt32(item.Quantity);
                cthd.DonGia = item.giasanpham;
                cthd.ThanhTien = item.giasanpham * item.Quantity;
                var s = service.LaychitietSach_donDH(cthd.SachID);
                if (s != null)
                {
                    s.SoLuongBan = s.SoLuongBan - cthd.SoLuong;
                    s.DaBan = cthd.SoLuong;
                    if (s.SoLuongBan == 0)
                    {
                        s.HetHang = true;
                    }
                }
                service.ThemChitietDonDatHang(cthd.DatHangID, cthd.SachID, int.Parse(cthd.SoLuong.ToString()), double.Parse(cthd.DonGia.ToString()), double.Parse(cthd.ThanhTien.ToString()));
                
            }

            return RedirectToAction("SendMailCart");

        }
        public ActionResult SendMailCart()
        {
            CartIndexViewModel cart = new CartIndexViewModel { Cart = GetCart() };
            string name = Session["Account"].ToString();
            KhachHang usr = service.LayKhachHang(name);
            string Email = usr.Email.Trim();
            string chuoi = "";

            foreach (var item in cart.Cart.Lines)
            {
                chuoi += "Mã SP: " + item.masp + "\tTên SP: " + item.tensp + "\n";
            }
            chuoi += "\nTổng tiền:" + cart.Cart.tongtien();
           
            SendEmail(usr.Email, "GIỎ HÀNG CỦA QUÝ KHÁCH TẠI BOOKS SHOP", chuoi);
            GetCart().Clear();
            return RedirectToAction("Default", "Default");
        }
        public void SendEmail(string address, string subject, string message)
        {
            string email = "booksshop2204@gmail.com";
            string password = "nguyenhoangnam";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
    }
}
