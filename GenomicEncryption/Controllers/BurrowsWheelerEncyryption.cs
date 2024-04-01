using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System;
using System.IO;
using System.Text;
using GenomicEncryption.Models;
using System.Diagnostics;


namespace GenomicEncryption.Controllers
{
    public class BurrowsWheelerEncyryption
    {
        public struct Rotation
        {
            public int Index;
            public string Suffix;
        }

        // Bir metni Barrows Willer algoritmasıyla şifreleyen ve şifreli halini döndüren metot
        public EncryptViewMoel Encrypt(EncryptViewMoel model)
        {
            Stopwatch encryptionTimer = Stopwatch.StartNew();


            model.PlainText = "$" + model.PlainText;
            // Metnin tüm döngüsel kaydırmalarını oluştur
            int n = model.PlainText.Length;
            string[] rotations = new string[n];
            for (int i = 0; i < n; i++)
            {
                rotations[i] = model.PlainText.Substring(i) + model.PlainText.Substring(0, i);
            }

            // Döngüsel kaydırmaları sözlük sırasına göre sırala
            Array.Sort(rotations);

            // Son sütunu döndür
            string bwt = "";
            for (int i = 0; i < n; i++)
            {
                bwt += rotations[i][n - 1];
            }
            encryptionTimer.Stop();
            model.EncryptionTime = (encryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();
            // Sonuçları model içine atayarak döndür
            model.EncryptedText = bwt;
            return model;
        }


        // Bir metnin Barrows Willer algoritmasıyla şifresini çözen ve orijinal metni döndüren metot
        public EncryptViewMoel Decrypt(EncryptViewMoel model)
        {
            Stopwatch decryptionTimer = Stopwatch.StartNew();

            // Son sütunu al
            int n = model.EncryptedText.Length;
            char[] last = model.EncryptedText.ToCharArray();

            // İlk sütunu oluştur
            char[] first = last.OrderBy(c => c).ToArray();

            // İlk ve son sütun arasındaki eşleşmeleri bul
            int[] next = new int[n];
            bool[] used = new bool[n];
            for (int i = 0; i < n; i++)
            {
                char c = first[i];
                for (int j = 0; j < n; j++)
                {
                    if (last[j] == c && !used[j])
                    {
                        next[i] = j;
                        used[j] = true;
                        break;
                    }
                }
            }

            // Orijinal metni bul
            string text = "";
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                text += first[index];
                index = next[index];
            }
            decryptionTimer.Stop();
            model.DecryptionTime = (decryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();
            // Sonucu model içine atayarak döndür
            text = text.TrimStart('$');
            model.PlainText = text;
            return model;
        }



    }
}
