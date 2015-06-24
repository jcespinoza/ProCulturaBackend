using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using ProCulturaBackEnd.Models;

namespace ProCulturaBackEnd.Controllers
{
    public class RegisterController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;
        public RegisterController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository, IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;

        }


        // POST api/register
        public RegisterModel Post([FromBody]UserModel model)
        {
            if (_readOnlyRepository.FirstOrDefault<User>(x => x.Email == model.Email) != null)
            {
                return new RegisterModel()
                {
                    Mensaje = "User already exists!",
                    Status = 500
                };
            }


            var newUser = _mappingEngine.Map<UserModel, User>(model);

            _writeOnlyRepository.Create(newUser);

            return new RegisterModel()
            {
                Email = newUser.Email,
                Mensaje = "Registration Complete!",
                Name = newUser.Name,
                Status = 200
            };
        }
    }
}
