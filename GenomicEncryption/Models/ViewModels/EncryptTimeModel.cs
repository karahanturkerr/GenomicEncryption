using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models.ViewModels
{
    public class EncryptTimeModel
    {
        public string AlgoName { get; set; }
        public bool IsEncrypt { get; set; }
        public double Time {  get; set; }

    }
}