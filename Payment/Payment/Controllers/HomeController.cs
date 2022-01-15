using Payment.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payment.Controllers
{
    public class HomeController : Controller
    {
        DoAnCuoiKiiEntities1 da = new DoAnCuoiKiiEntities1();

        
        public ActionResult Index()
        {
            var sp = da.Sanphams.ToList();

            return View(sp);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}