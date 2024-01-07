using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System;
using System.IO;
using System.Text;
using GenomicEncryption.Models;


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
        public string BurrowsWheelerEncryption(string inputText)
        {
            // '$' karakterini metnin sonuna ekleyerek devam et
            inputText += "$";

            int lenText = inputText.Length;

            // Array of structures to store rotations and their indexes
            Rotation[] suff = new Rotation[lenText];

            // Structure is needed to maintain old indexes of rotations
            // after sorting them
            for (int i = 0; i < lenText; i++)
            {
                suff[i].Index = i;
                suff[i].Suffix = inputText.Substring(i);
            }

            // Sorts rotations using comparison function
            Array.Sort(suff, (x, y) => string.Compare(x.Suffix, y.Suffix));

            // Stores the indexes of sorted rotations
            int[] suffixArr = new int[lenText];
            for (int i = 0; i < lenText; i++)
            {
                suffixArr[i] = suff[i].Index;
            }

            // Iterates over the suffix array to find the last char of each cyclic rotation
            char[] bwtArr = new char[lenText];
            for (int i = 0; i < lenText; i++)
            {
                // Computes the last char which is given by inputText[(suffixArr[i] + lenText - 1) % lenText]
                int j = (suffixArr[i] + lenText - 1) % lenText;
                bwtArr[i] = inputText[j];
            }

            // Returns the computed Burrows-Wheeler Transform
            return new string(bwtArr);
        }

        // Bir metnin Barrows Willer algoritmasıyla şifresini çözen ve orijinal metni döndüren metot
        static string IBWT(string bwt)
        {
            // Son sütunu al
            int n = bwt.Length;
            char[] last = bwt.ToCharArray();

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

            return text;
        }


    }
}
