using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models.Classes
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Eposta { get; set; }
        public string Text { get; set; }
        public ICollection<userMessage> UserMessages { get; set; }
    }
}