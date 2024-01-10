using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models.Classes
{
    public class userMessage
    {
        [Key]
        public int Id { get; set; }
        public Users User { get; set; }
        public int UserId { get; set; }
        public Messages Message { get; set; }
        public int MessageId { get; set; }
    }
}