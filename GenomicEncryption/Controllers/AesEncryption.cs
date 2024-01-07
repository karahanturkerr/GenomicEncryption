using GenomicEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GenomicEncryption.Controllers
{
    public class AesEncryption
    {
        public EncryptViewMoel Encrypt(EncryptViewMoel model)
        {
            // AES nesnesi oluştur
            using (Aes aes = Aes.Create())
            {
                // Anahtar ve IV'yi rastgele üret
                aes.GenerateKey();
                aes.GenerateIV();

                // Şifreleme için bir transform nesnesi oluştur
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encrypted;

                // Metni byte dizisine dönüştür
                byte[] plainBytes = Encoding.UTF8.GetBytes(model.PlainText);

                // Metni şifrele ve sonucu byte dizisine ata
                encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                // Şifreli metnin başına anahtar ve IV'yi ekle
                byte[] result = new byte[aes.Key.Length + aes.IV.Length + encrypted.Length];
                Buffer.BlockCopy(aes.Key, 0, result, 0, aes.Key.Length);
                Buffer.BlockCopy(aes.IV, 0, result, aes.Key.Length, aes.IV.Length);
                Buffer.BlockCopy(encrypted, 0, result, aes.Key.Length + aes.IV.Length, encrypted.Length);
                model.EncryptedText = Convert.ToBase64String(result);   
                return model;
            }
        }


        public EncryptViewMoel Decrypt(EncryptViewMoel model)
        {
            // Şifreli metni Base64 formatından byte dizisine dönüştür
            byte[] cipherBytes = Convert.FromBase64String(model.EncryptedText);

            // Anahtar ve IV'yi şifreli metnin başından al
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            Buffer.BlockCopy(cipherBytes, 0, key, 0, key.Length);
            Buffer.BlockCopy(cipherBytes, key.Length, iv, 0, iv.Length);

            // Şifreli metnin geri kalanını al
            byte[] encrypted = new byte[cipherBytes.Length - key.Length - iv.Length];
            Buffer.BlockCopy(cipherBytes, key.Length + iv.Length, encrypted, 0, encrypted.Length);

            using (Aes aes = Aes.Create())
            {
                // Şifreleme için bir transform nesnesi oluştur
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                // Şifresi çözülen metni tutacak bir byte dizisi oluştur
                byte[] decrypted;

                try
                {
                    // Metnin şifresini çöz ve sonucu byte dizisine ata
                    decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
                }
                catch (CryptographicException)
                {
                    // Eğer anahtar veya IV geçersizse, sahte bir metin üret
                    decrypted = GenerateFakeText(encrypted.Length);
                }
                model.PlainText = Encoding.UTF8.GetString(decrypted);
                // Sonucu string formatında döndür
                return model;
            }
        }


        public byte[] GenerateFakeText(int length)
        {
            // Türkçe karakterlerin ASCII kodlarını tutan bir dizi oluştur
            byte[] turkishChars = new byte[] { 199, 231, 208, 240, 221, 253, 214, 246, 220, 252, 222, 254 };

            // Rastgele sayı üretmek için bir nesne oluştur
            Random random = new Random();

            // Sahte metni tutacak bir byte dizisi oluştur
            byte[] fakeText = new byte[length];

            // Her bir byte için rastgele bir karakter seç
            for (int i = 0; i < length; i++)
            {
                // %10 olasılıkla Türkçe bir karakter seç
                if (random.Next(10) == 0)
                {
                    fakeText[i] = turkishChars[random.Next(turkishChars.Length)];
                }
                // %90 olasılıkla İngilizce bir karakter seç
                else
                {
                    fakeText[i] = (byte)random.Next(65, 91);
                }
            }

            return fakeText;
        }
    }
}