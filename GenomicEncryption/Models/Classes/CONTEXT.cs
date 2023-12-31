using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GenomicEncryption.Models.Classes
{
    public class CONTEXT : DbContext
    {
        public DbSet<GenomicCodes> GenomicCodes { get; set; }
    }
}