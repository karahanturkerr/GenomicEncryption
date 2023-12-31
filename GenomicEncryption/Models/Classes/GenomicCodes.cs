using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace GenomicEncryption.Models.Classes
{
    public class GenomicCodes
    {
        [Key]
        public int ID { get; set; }
        public string DEGER { get; set; }
    }
}