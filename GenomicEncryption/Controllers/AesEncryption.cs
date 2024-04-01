using GenomicEncryption.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch encryptionTimer = Stopwatch.StartNew();

            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encrypted;

                byte[] plainBytes = Encoding.UTF8.GetBytes(model.PlainText);

                encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                byte[] result = new byte[aes.Key.Length + aes.IV.Length + encrypted.Length];
                Buffer.BlockCopy(aes.Key, 0, result, 0, aes.Key.Length);
                Buffer.BlockCopy(aes.IV, 0, result, aes.Key.Length, aes.IV.Length);
                Buffer.BlockCopy(encrypted, 0, result, aes.Key.Length + aes.IV.Length, encrypted.Length);
                model.EncryptedText = Convert.ToBase64String(result);
            }

            encryptionTimer.Stop();
            model.EncryptionTime = (encryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();

            return model;
        }


        public EncryptViewMoel Decrypt(EncryptViewMoel model)
        {
            Stopwatch decryptionTimer = Stopwatch.StartNew();

            byte[] cipherBytes = Convert.FromBase64String(model.EncryptedText);

            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            Buffer.BlockCopy(cipherBytes, 0, key, 0, key.Length);
            Buffer.BlockCopy(cipherBytes, key.Length, iv, 0, iv.Length);

            byte[] encrypted = new byte[cipherBytes.Length - key.Length - iv.Length];
            Buffer.BlockCopy(cipherBytes, key.Length + iv.Length, encrypted, 0, encrypted.Length);

            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                byte[] decrypted;

                try
                {
                    decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
                }
                catch (CryptographicException)
                {
                    decrypted = GenerateFakeText(encrypted.Length);
                }
                model.PlainText = Encoding.UTF8.GetString(decrypted);
            }

            decryptionTimer.Stop();
            model.DecryptionTime = (decryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();

            return model;
        }


        public byte[] GenerateFakeText(int length)
        {
            byte[] turkishChars = new byte[] { 199, 231, 208, 240, 221, 253, 214, 246, 220, 252, 222, 254 };

            Random random = new Random();

            byte[] fakeText = new byte[length];

            for (int i = 0; i < length; i++)
            {
                if (random.Next(10) == 0)
                {
                    fakeText[i] = turkishChars[random.Next(turkishChars.Length)];
                }
                else
                {
                    fakeText[i] = (byte)random.Next(65, 91);
                }
            }

            return fakeText;
        }
    }
}