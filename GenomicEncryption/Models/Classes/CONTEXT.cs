using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GenomicEncryption.Models.Classes
{

    public class CONTEXT : DbContext
    {
        public CONTEXT() 
            : base("name=CONTEXT2") { }
        
        public DbSet<GenomicCodes> GenomicCodes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<GenomicCodesTimes> GenomicCodesTimes { get; set; }
    }
}