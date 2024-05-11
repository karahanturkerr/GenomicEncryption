using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Controllers
{
    public class DataSimilarity
    {
        static void Main(string[] args)
        {
            string metin1 = "abc";
            string metin2 = "abccc";

            int eslesenHarfSayisi = HarfEslesmeSay(metin1, metin2);

            int maxHarfSayisi = Math.Max(metin1.Length, metin2.Length);

            double benzerlikOrani = (double)eslesenHarfSayisi / maxHarfSayisi * 100;

            Console.WriteLine("Benzerlik oranı: %" + benzerlikOrani);

            Console.WriteLine("Eşleşen harf sayısı: " + eslesenHarfSayisi);
        }


        static int HarfEslesmeSay(string metin1, string metin2)
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