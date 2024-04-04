using GenomicEncryption.Models;
using GenomicEncryption.Models.Classes;
using GenomicEncryption.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace GenomicEncryption.Controllers
{
    public class EncryptionController : Controller
    {
        CONTEXT db = new CONTEXT();

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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult LogTime(string AlgorithmName, bool IsEncrypt, double Time)
        {


            var genomicCodesTime = new GenomicCodesTimes
            {
                AlgorithmName = AlgorithmName,
                IsEncrypt = IsEncrypt,
                Time = Time,
                CreatedDate = DateTime.Now,
            };

            db.GenomicCodesTimes.Add(genomicCodesTime);
            db.SaveChanges();

            return Json(new { success = true });
        }

        [HttpGet]
        public JsonResult GetExistingData()
        {
            var existingData = db.GenomicCodesTimes.ToList();
            return Json(setGraphData(existingData), JsonRequestBehavior.AllowGet);
        }


        private GraphViewModel setGraphData(List<GenomicCodesTimes> list)
        {
            var graphModel = new GraphViewModel();
            graphModel.AesTrueCounts = GetFilteredData(list, "AesSifreleme", true);
            graphModel.AesFalseCounts = GetFilteredData(list, "AesSifreleme", false);
            graphModel.BurrowsTrueCounts = GetFilteredData(list, "BurrowsWheelerSifreleme", true);
            graphModel.BurrowsFalseCounts = GetFilteredData(list, "BurrowsWheelerSifreleme", false);
            graphModel.TripleDESTrueCounts = GetFilteredData(list, "TripleDESSifreleme", true);
            graphModel.TripleDESFalseCounts = GetFilteredData(list, "TripleDESSifreleme", false);

            return graphModel;

        }


        private List<double> GetFilteredData(List<GenomicCodesTimes> list, string functionName, bool isEncrypt)
        {
            
            var newList = list
                  .Where(gc => gc.AlgorithmName == functionName && gc.IsEncrypt == isEncrypt)
                  .OrderByDescending(o => o.CreatedDate)
                  .Take(10)
                  .Select(gc => gc.Time)
                  .ToList();
            while (newList.Count < 10)
            {
                newList.Add(0);

            }

            return newList;

        }

    }
}