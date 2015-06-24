using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Entities;
using ProCulturaBackEnd.Models;
using Domain.Services;
using ProCulturaBackEnd.App_Start;


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


        // POST api/values
        public AuthModel Post([FromBody]UserModel model)
        {
            var user = _readOnlyRepository.FirstOrDefault<User>(x => x.Email == model.Email);
            if (!user.CheckPassword(model.Password))
                return new AuthModel()
                {
                    Mensaje = "Login Failed",
                    Status = 403
                };

            var authModel = new AuthModel
            {
                email = user.Email,
                access_token = AuthRequestFactory.BuildEncryptedRequest(user.Email),
                Mensaje = "Login Succesfully",
                Status = 200
            };

            return authModel;
        }
    }
}
