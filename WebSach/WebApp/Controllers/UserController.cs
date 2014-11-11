using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.ServiceReference1;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
       
        Service1Client service = new Service1Client();
        // GET: User
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(FormCollection f)
        {
            string Email = f["email"];
          

            var usr = service.ThongTinKhachHang(Email).ToList();
            string pwd = "";
            string usrname = "";
            string chuoi = "";
            foreach (KhachHang k in usr)
            {
                pwd = k.MatKhau;
                usrname = k.TenDangNhap;
            }

            chuoi += "Tên đăng nhập:" + usrname + " ";
            chuoi += "\n Mật khẩu: " + pwd + " ";

            string mail = "Chào Email: " + Email + chuoi;
            SendEmail(Email, "Books Shop", mail);

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


        /// Đăng Kí
        [HttpGet]
        public ActionResult FormRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FormRegister(FormCollection f)
        {
            int ngay = int.Parse(f["ngay"]);
            int thang = int.Parse(f["thang"]);
            int nam = int.Parse(f["nam"]);
            DateTime ns = new DateTime(nam, thang, ngay);
            KhachHang usr = new KhachHang();
            usr.TenDangNhap = f["name"];
            usr.MatKhau = f["pass"];
            usr.HoTen = f["name2"];
            usr.Email = f["email"];
            usr.DienThoai = f["phone"];
            usr.DiaChi = f["city"];
            
            usr.NgaySinh = ns;
           
            if (ModelState.IsValid)
            {
                
                {
                    string name = f["name"];
                    bool userInValid = service.KiemtraDk(name);
                    if (userInValid)
                    {
                        ViewBag.Mess = "Tên đăng nhập đã tồn tại!";
                    }
                    else
                    {

                        service.ThemKhacHang(usr.HoTen,usr.DiaChi,usr.DienThoai,usr.TenDangNhap,usr.MatKhau,ns,usr.GioiTinh,usr.Email);
                        return RedirectToAction("Default", "Default");

                    }
                }

            }
            ViewBag.ThongTinUsr = usr;
            return View(usr);
        }
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f, string returnUrl)
        {
            if (ModelState.IsValid)
            {
              
                    string username = f["username"];
                    string password = f["txtpass"];
                    string _remember = f["remember"];
                    bool remember;
                    if (_remember == null)
                    {
                        remember = false;
                    }
                    else
                    {
                        remember = true;
                    }
                    var kh = service.KiemTraDangNhap(username, password);

                    bool userValid = kh.Any();

                    if (userValid)
                    {
                        foreach (KhachHang a in kh)
                        {
                            if (a.Active == null)
                            {
                                if (a.Admin == true)
                                {
                                    Session["Account"] = username;
                                    FormsAuthentication.SetAuthCookie(username, remember);

                                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                                    {
                                        return Redirect(returnUrl);
                                    }
                                    else
                                    {
                                                                  
                                        return RedirectToAction("Default", "Default");
                                        
                                    }
                                }
                                else
                                {
                                    Session["Account"] = username;
                                    FormsAuthentication.SetAuthCookie(username,remember);

                                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                                    {
                                        return Redirect(returnUrl);
                                    }
                                    else
                                    {
                                        
                                        return RedirectToAction("Default", "Default");
                                       
                                    }
                                }
                            }
                            else
                            {
                                ViewBag.ThongBao = "User đã bị xoá!";
                            }

                        }



                    }
                    else
                    {

                        return RedirectToAction("Default", "Default");
                      
                    }
                
            }
            return View(f);
        }
        public ActionResult LogOff()
        {
            Session["Account"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Default", "Default");
        }

    }
}