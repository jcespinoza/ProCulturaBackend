using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Entities;
using ProCulturaBackEnd.Models;
using Domain.Services;


namespace ProCulturaBackEnd.Controllers
{
    public class LoginController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;

        public LoginController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
        }
        // GET api/values/5
        public UserModel Get(int id)
        {
            return new UserModel()
            {
                id = 1,
                Email = "lkajdalkj",
                Name = "hello",
                Password = "dskjshfjdh"
            };
        }

        // POST api/values
        public UserModel Post([FromBody]UserModel model)
        {
            var user = _readOnlyRepository.FirstOrDefault<User>(x => x.Email == model.Email);
            if (user.CheckPassword(model.Password))
            {
                
            }
            return model;
        }
    }
}
