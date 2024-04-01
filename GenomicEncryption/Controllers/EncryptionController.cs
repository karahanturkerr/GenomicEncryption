using GenomicEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace GenomicEncryption.Controllers
{
    public class EncryptionController : Controller
    {
        [HttpPost]
        public JsonResult IndexEncrypt(EncryptViewMoel model)
        {
            AesEncryption aes = new AesEncryption();
            var result = aes.Encrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult IndexDecrypt(EncryptViewMoel model)
        {
            AesEncryption aes = new AesEncryption();
            var result = aes.Decrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IndexEncryptBurrows(EncryptViewMoel model)
        {
            BurrowsWheelerEncyryption brw = new BurrowsWheelerEncyryption();
            var result = brw.Encrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IndexDecryptBurrows(EncryptViewMoel model)
        {
            BurrowsWheelerEncyryption brw = new BurrowsWheelerEncyryption();
            var result = brw.Decrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IndexEncryptTripleDES(EncryptViewMoel model)
        {
            TripleDESEncryption brw = new TripleDESEncryption();
            var result = brw.Encrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IndexDecryptTripleDES(EncryptViewMoel model)
        {
            TripleDESEncryption brw = new TripleDESEncryption();
            var result = brw.Decrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}