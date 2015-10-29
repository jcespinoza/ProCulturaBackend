using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProCultura.WebApiOwin.Models
{
    public class Privilege
    {
        [Key] 
        public string PrivilegeId { get; set; }
        public string Action { get; set; }

    }
}