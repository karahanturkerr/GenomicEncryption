using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models.ViewModels
{
    public class GraphViewModel
    {
        public List<double> AesTrueCounts  { get; set; }

        public List<double> AesFalseCounts { get; set; }

        public List<double> BurrowsTrueCounts { get; set; }

        public List<double> BurrowsFalseCounts { get; set; }

        public List<double> TripleDESTrueCounts { get; set; }

        public List<double> TripleDESFalseCounts { get; set; }

        public List<double> TwofishTrueCounts { get; set; }

        public List<double> TwofishFalseCounts { get; set; }


    }
}