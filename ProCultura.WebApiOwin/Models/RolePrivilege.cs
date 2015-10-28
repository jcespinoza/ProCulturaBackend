using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProCultura.WebApiOwin.Models
{
    public class RolePrivilege
    {
        public string PrivilegeId { get; set; }
        public string RoleId { get; set; }

        public virtual Privilege Privilege { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}
