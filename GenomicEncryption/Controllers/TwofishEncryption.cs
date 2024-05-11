using System;
using GenomicEncryption.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace GenomicEncryption.Controllers
{
    public class TwofishEncryption
    {
        private static byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
        private static byte[] iv = Encoding.UTF8.GetBytes("ABCDEF0123456789");

        public EncryptViewMoel Encrypt(EncryptViewMoel model)
        {
            Stopwatch encryptionTimer = Stopwatch.StartNew();

            byte[] plainBytes = Encoding.UTF8.GetBytes(model.PlainText);

            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new TwofishEngine()));
            cipher.Init(true, new ParametersWithIV(new KeyParameter(key), iv));

            byte[] encryptedBytes = new byte[cipher.GetOutputSize(plainBytes.Length)];
            int length = cipher.ProcessBytes(plainBytes, 0, plainBytes.Length, encryptedBytes, 0);
            length += cipher.DoFinal(encryptedBytes, length);

            model.EncryptedText = Convert.ToBase64String(encryptedBytes);

            encryptionTimer.Stop();
            model.EncryptionTime = (encryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();

            return model;
        }

        public EncryptViewMoel Decrypt(EncryptViewMoel model)
        {
            Stopwatch decryptionTimer = Stopwatch.StartNew();

            byte[] cipherBytes = Convert.FromBase64String(model.EncryptedText);

            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new TwofishEngine()));
            cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));

            byte[] decryptedBytes = new byte[cipher.GetOutputSize(cipherBytes.Length)];
            int length = cipher.ProcessBytes(cipherBytes, 0, cipherBytes.Length, decryptedBytes, 0);
            length += cipher.DoFinal(decryptedBytes, length);

            model.PlainText = Encoding.UTF8.GetString(decryptedBytes);

            decryptionTimer.Stop();
            model.DecryptionTime = (decryptionTimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))).ToString();

            return model;
        }
    }
}