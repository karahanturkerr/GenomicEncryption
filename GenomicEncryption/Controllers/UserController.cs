using GenomicEncryption.Models.Classes;
using GenomicEncryption.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GenomicEncryption.Controllers
{
    public class UserController : Controller
    {
        CONTEXT db = new CONTEXT();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {

                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    TempData["SuccessMessage"] = "Başarıyla giriş yaptınız!";
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Login", "User");
            }

            return RedirectToAction("Login", "User");

        }


        [HttpGet]//sadece ilgili ekranı görmek istediğimde getle yapıyorum
        public ActionResult Register()
        {
            return View();

        }

        [HttpPost]//Kullanıcıdan veri alırken post attribute kullanırız
        public ActionResult Register(Users p1)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }
            db.Users.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}

