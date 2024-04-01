using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenomicEncryption.Models.Classes;

namespace GenomicEncryption.Controllers
{
    public class HomeController : Controller
    {
        CONTEXT db = new CONTEXT();
        public ActionResult Index()
        {
            return View();
        }

        

        [HttpGet]//sadece ilgili ekranı görmek istediğimde getle yapıyorum
        public ActionResult Contact()
        {
            return View();

        }

        [HttpPost]//Kullanıcıdan veri alırken post attribute kullanırız
        public ActionResult Contact(Messages p1)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Project");
            }
            db.Messages.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }



        public ActionResult Project()
        {
            ViewBag.Message = "Your project page.";

            return View();
        }
     
       
        public ActionResult AesSifreleme()
        {
            var degerler = db.GenomicCodes.ToList();
            return View(degerler);
        }

        public ActionResult BurrowsWheelerSifreleme()
        {
            var degerler = db.GenomicCodes.ToList();
            return View(degerler);

        }

        public ActionResult TripleDESSifreleme()
        {
            var degerler = db.GenomicCodes.ToList();
            return View(degerler);

        }


        public ActionResult VeriTabaniGoruntule()
        {
            var degerler = db.GenomicCodes.ToList();
            return View(degerler);
        }


        [HttpGet]//sadece ilgili ekranı görmek istediğimde getle yapıyorum
        public ActionResult KodEkle()
        {
            return View();

        }

        [HttpPost]//Kullanıcıdan veri alırken post attribute kullanırız
        public ActionResult KodEkle(GenomicCodes p1)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("KodEkle");
            }
            db.GenomicCodes.Add(p1);
            db.SaveChanges();
            return RedirectToAction("KodEkle");
        }


    }
}