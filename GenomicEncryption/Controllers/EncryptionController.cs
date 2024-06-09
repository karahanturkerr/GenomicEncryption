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
            TripleDESEncryption trp = new TripleDESEncryption();
            var result = trp.Encrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IndexDecryptTripleDES(EncryptViewMoel model)
        {
            TripleDESEncryption trp = new TripleDESEncryption();
            var result = trp.Decrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult IndexEncryptTwofish(EncryptViewMoel model)
        {
            TwofishEncryption twf = new TwofishEncryption();
            var result = twf.Encrypt(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IndexDecryptTwofish(EncryptViewMoel model)
        {
            TwofishEncryption twf = new TwofishEncryption();
            var result = twf.Decrypt(model);
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

        [HttpPost]
        public ActionResult DataSave(string AD, string DEGER)
        {


            var genomicCodes = new GenomicCodes
            {
                AD = AD,
                DEGER = DEGER,
            };

            db.GenomicCodes.Add(genomicCodes);
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
            graphModel.TwofishTrueCounts = GetFilteredData(list, "TwofishSifreleme", true);
            graphModel.TwofishFalseCounts = GetFilteredData(list, "TwofishSifreleme", false);

            return graphModel;

        }


        private List<double> GetFilteredData(List<GenomicCodesTimes> list, string functionName, bool isEncrypt)
        {

            var newList = list
                  .Where(gc => gc.AlgorithmName == functionName && gc.IsEncrypt == isEncrypt)
                  .OrderByDescending(o => o.CreatedDate)
                  .Take(20)
                  .Select(gc => gc.Time)
                  .ToList();
            while (newList.Count < 20)
            {
                newList.Add(0);

            }

            return newList;

        }

        [HttpPost]
        public ActionResult EncryptedDataSave(string AlgoritmaAdi, string SifrelenmisVeri)
        {
            // Veritabanında aynı şifrelenmiş verinin olup olmadığını kontrol et
            bool varMi = db.EncryptedData.Any(e => e.SifrelenmisVeri == SifrelenmisVeri);

            if (!varMi)
            {
                var encryptedData = new EncryptedData
                {
                    AlgoritmaAdi = AlgoritmaAdi,
                    SifrelenmisVeri = SifrelenmisVeri,
                };

                db.EncryptedData.Add(encryptedData);
                db.SaveChanges();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Bu şifrelenmiş veri zaten var." });
            }
        }

        [HttpGet]
        public JsonResult GetDataByAlgorithm(string algorithm = "")
        {
            if (string.IsNullOrEmpty(algorithm))
            {
                algorithm = "AesSifreleme"; // Varsayılan olarak AesSifreleme seçili olacak
            }
            var encryptedDataList = db.EncryptedData.Where(e => e.AlgoritmaAdi == algorithm).ToList();
            return Json(encryptedDataList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CalculateSimilarity(string text1, string text2)
        {
            var (score, align1, align2, percentage) = SmithWatermanHelper.CalculateSmithWaterman(text1, text2);

            return Json(new
            {
                score = score,
                alignment1 = align1,
                alignment2 = align2,
                similarityPercentage = percentage
            });
        }

        private int HarfEslesmeSay(string metin1, string metin2)
        {
            int eslesenHarfSayisi = 0;
            int minUzunluk = Math.Min(metin1.Length, metin2.Length);

            for (int i = 0; i < minUzunluk; i++)
            {
                if (metin1[i] == metin2[i])
                {
                    eslesenHarfSayisi++;
                }
            }

            return eslesenHarfSayisi;
        }






    }
}