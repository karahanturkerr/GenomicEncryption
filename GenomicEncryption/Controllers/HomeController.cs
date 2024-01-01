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
        public ActionResult Project()
        {
            ViewBag.Message = "Your project page.";

            return View();
        }
        public ActionResult Team()
        {
            ViewBag.Message = "Your project page.";

            return View();
        }
        public ActionResult AesSifreleme()
        {
            ViewBag.Message = "Your Aes Sifreleme page.";

            return View();
        }
        public ActionResult BurrowsWheelerSifreleme()
        {
            ViewBag.Message = "Your Aes Sifreleme page.";

            return View();
        }
    }
}