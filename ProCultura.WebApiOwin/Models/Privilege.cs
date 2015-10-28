using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProCultura.WebApiOwin.Models
{
    public class Privilege
    {
        public string PrivilegeId { get; set; }
        public string Action { get; set; }
        public IdentityRole Role { get; set; }
    }
}