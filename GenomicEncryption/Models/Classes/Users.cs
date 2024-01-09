using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenomicEncryption.Models.Classes
{
    public class Users
    {

        [Key]
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string PasswordConfirm { get; set; }

    }
}