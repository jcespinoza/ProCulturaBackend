using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProCulturaBackEnd.Models
{
    public class AuthModel : ResponseModel
    {
        public string email { get; set; }
        public string access_token { get; set; }
    }
}