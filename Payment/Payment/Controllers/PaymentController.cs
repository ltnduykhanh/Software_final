using Payment.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Text;
using Payment.Models;

namespace Payment.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        DoAnCuoiKiiEntities1 da = new DoAnCuoiKiiEntities1();
        //[HttpPost]
        public ActionResult Index(string masanpham)
        {
           
            if(masanpham == null)
            {
                return null;
            }
            else
            {
                var pro = da.Sanphams.Where(x => x.maSanpham == masanpham).FirstOrDefault();
                Donhang obj = new Donhang();
                obj.maDonhang = random_syntax();
              

                obj.tensanpham = pro.tensanpham;
                obj.gia = pro.giatien;


                //obj.tenDaily = Request["tmp"].ToString();


                obj.tenDaily = Request.Form["tmp"];
                obj.soluong = 0;
              
                
                da.Donhangs.Add(obj);
                da.SaveChanges();
                return View(pro);
            }

        }
       
        public string random_syntax()
        {
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            int length = 16;
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}