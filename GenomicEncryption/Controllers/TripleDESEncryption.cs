using GenomicEncryption.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class TripleDESEncryption
{
    public EncryptViewMoel Encrypt(EncryptViewMoel model)
    {
        // TripleDES nesnesi oluştur ve anahtar/IV üret
        using (TripleDES tripleDES = TripleDES.Create())
        {
            byte[] key = Encoding.UTF8.GetBytes("123456789012345678901234");
            byte[] iv = Encoding.UTF8.GetBytes("12345678");


            // Zamanlayıcıyı başlat
            Stopwatch encryptionTimer = Stopwatch.StartNew();

            // Metni şifrele
            byte[] encryptedText = EncryptStringToBytes(model.PlainText, key, iv);

            // Zamanlayıcıyı durdur ve süreyi kaydet
            encryptionTimer.Stop();
            model.EncryptionTime = (encryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();

            // Sonuçları model içine kaydet
            model.EncryptedText = Convert.ToBase64String(encryptedText);
        }
        return model;
    }

    public EncryptViewMoel Decrypt(EncryptViewMoel model)
    {
        // Base64 formatındaki şifreli metni byte dizisine dönüştür
        byte[] cipherBytes = Convert.FromBase64String(model.EncryptedText);

        // Anahtar ve IV'yi al
        byte[] key = Encoding.UTF8.GetBytes("123456789012345678901234");
        byte[] iv = Encoding.UTF8.GetBytes("12345678");

        // Zamanlayıcıyı başlat
        Stopwatch decryptionTimer = Stopwatch.StartNew();

        string decryptedText;
        using (TripleDES tripleDES = TripleDES.Create())
        {
            tripleDES.Key = key;
            tripleDES.IV = iv;

            decryptedText = DecryptStringFromBytes(cipherBytes, tripleDES);
        }

        // Zamanlayıcıyı durdur ve süreyi kaydet
        decryptionTimer.Stop();
        model.DecryptionTime = (decryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();

        // Sonuçları model içine kaydet
        model.PlainText = decryptedText;

        return model;
    }

    private byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (TripleDES tripleDES = TripleDES.Create())
            {
                tripleDES.Key = key;
                tripleDES.IV = iv;

                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, tripleDES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
                    csEncrypt.Write(plaintextBytes, 0, plaintextBytes.Length);
                    csEncrypt.FlushFinalBlock();
                }
            }

            return msEncrypt.ToArray();
        }
    }

    private string DecryptStringFromBytes(byte[] cipherText, TripleDES tripleDES)
    {
        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, tripleDES.CreateDecryptor(), CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
