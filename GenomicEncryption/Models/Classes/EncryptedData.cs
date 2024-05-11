using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models.Classes
{
    public class EncryptedData
    {
        [Key]
        public int ID { get; set; }
        public string AlgoritmaAdi { get; set; }
        public string SifrelenmisVeri { get; set; }
    }
}