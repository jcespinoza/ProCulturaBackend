using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProCulturaBackEnd.Models
{
    public class RegisterModel : ResponseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

    }
}