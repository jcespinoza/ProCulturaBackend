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
        [ForeignKey("PrivilegeId")]
        public Privilege Privilege { get; set; }

        [KeyAttribute]
        public string PrivilegeId { get; set; }

        [KeyAttribute]
        public string RoleId { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        public IdentityRole Role { get; set; }
    }
}
