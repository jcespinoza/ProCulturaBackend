using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProCultura.WebApiOwin.Models
{
    public class RolePrivilege
    {
        [Required]
        [ForeignKey("Privilege")]
        public Privilege Privilege { get; set; }

        [Required]
        [ForeignKey("IdentityRole")]
        public IdentityRole Role { get; set; }
    }
}
