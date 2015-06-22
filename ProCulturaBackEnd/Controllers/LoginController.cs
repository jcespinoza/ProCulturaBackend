using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProCulturaBackEnd.Models;

namespace ProCulturaBackEnd.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/values/5
        public UserModel Get(int id)
        {
            return new UserModel()
            {
                id = 1,
                UserName = "hello",
                Password = "dskjshfjdh"
            };
        }

        // POST api/values
        public UserModel Post([FromBody]UserModel model)
        {
            return model;
        }
    }
}
