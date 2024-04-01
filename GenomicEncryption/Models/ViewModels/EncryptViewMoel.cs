using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models
{
    public class EncryptViewMoel
    {
        public string PlainText { get; set; }
        public string EncryptedText { get; set; }
        public string EncryptionTime { get; set; }
        public string DecryptionTime { get; set; }
    }
}