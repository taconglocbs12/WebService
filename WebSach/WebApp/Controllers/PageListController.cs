using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class PageListController : Controller
    {

        [ChildActionOnly]
        public ActionResult PhanTrang(int page = 1, string currentpage = null, int totalpages = 1)
        {
            ViewBag.currentpage = currentpage;
            ViewBag.Page = page;
            ViewBag.TotalPages = totalpages;
            return PartialView();
        }

    }
}
