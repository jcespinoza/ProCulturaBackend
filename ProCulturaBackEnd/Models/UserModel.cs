using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProCulturaBackEnd.Models
{
    [Table("userdetails")]
    public class UserModel
    {
        [Key]
        public int id { get; set; }
        public string Password { get; set; }
        public  string Name { get; set; }
        public  string Email { get; set; }
    }
}