using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models.Classes
{
    public class GenomicCodesTimes
    {
        [Key]
        public int ID { get; set; }
        public string AlgorithmName { get; set; }
        public bool IsEncrypt { get; set; }
        public double Time {  get; set; }
        public DateTime CreatedDate { get; set; }
    }
}