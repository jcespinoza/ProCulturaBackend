﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ProCulturaBackEnd.Contexts;
using ProCulturaBackEnd.Entities;
using ProCulturaBackEnd.L10N;
using ProCulturaBackEnd.Models;
using ProCulturaBackEnd.Services;
using AutoMapper;

namespace ProCulturaBackEnd.Controllers
{
    public class UserController : ApiController
    {
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        // PUT api/user/5
        public IHttpActionResult PutUser(string token, UserModel recievedUser) 
        {
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            var user = Mapper.Map<UserEntity>(recievedUser);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tokenModel = AuthRequestFactory.BuildDecryptedRequest(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);
            if (requestSendingUser == null)
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.AuthRequestNotRecognized());
            if (requestSendingUser.Id != user.Id && requestSendingUser.Role <= user.Role) //check for clearance
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.InsufficientPrivileges());
            var obtaineduser = _db.UserModels.FirstOrDefault(x => x.Id == user.Id);
            if (obtaineduser != null)
            {
                /*if (!PasswordEncryptionService.CheckPassword(obtaineduser, user.Password) && obtaineduser.Password != user.Password)
                {
                    PasswordEncryptionService.Encrypt(user);
                }*/
                user.Password = obtaineduser.Password;
                user.Salt = obtaineduser.Salt;
                _db.Entry(obtaineduser).CurrentValues.SetValues(user);
            }
            else
                return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFound());
            _db.SaveChanges();
            return new HttpActionResult(HttpStatusCode.OK, LocalizedResponseService.LocalizedResponseFactory.UpdateUserSuccessMessage());
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token, int id)
        {
            var tokenModel = AuthRequestFactory.BuildDecryptedRequest(token);
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            var obtaineduserEntity = _db.UserModels.FirstOrDefault(x => x.Id == id);
            var obtaineduser = Mapper.Map<UserModel>(obtaineduserEntity);
            var requestingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);
            if (obtaineduser == null || requestingUser == null) return NotFound();
            if (obtaineduserEntity == requestingUser)
                return Ok(obtaineduser);
            if(requestingUser.Role >= obtaineduser.Role)
                return Ok(obtaineduser);
            return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFound());
        }

        // POST api/user
      
        public IHttpActionResult PostUser(RegisterModel user)
        {
          if(_db.UserModels.FirstOrDefault(x => x.Email == user.Email) != null)
              return new HttpActionResult(HttpStatusCode.InternalServerError, LocalizedResponseService.LocalizedResponseFactory.EmailInUseMessage()); 
            if(!user.Password.Equals(user.ConfirmPassword))
                return new HttpActionResult(HttpStatusCode.InternalServerError, LocalizedResponseService.LocalizedResponseFactory.PasswordMismatchMessage());
            Mapper.CreateMap<RegisterModel, UserEntity>().ReverseMap();
            var newUser = Mapper.Map<UserEntity>(user);
            PasswordEncryptionService.Encrypt(newUser);
            _db.UserModels.Add(newUser);
            _db.SaveChanges();
            return new HttpActionResult(HttpStatusCode.OK, LocalizedResponseService.LocalizedResponseFactory.RegistrationSuccessMessage());
        }


        // DELETE api/user/5
        public IHttpActionResult DeleteUser(string token, int id)
        {
            var user = _db.UserModels.Find(id);
            if (user == null)
                return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFound());
            var tokenModel = AuthRequestFactory.BuildDecryptedRequest(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);
            if (requestSendingUser == null)
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.AuthRequestNotRecognized());
            if (requestSendingUser.Id != user.Id && requestSendingUser.Role <= user.Role) //check for clearance
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.InsufficientPrivileges());
            _db.UserModels.Remove(user);
            _db.SaveChanges();
            return new HttpActionResult(HttpStatusCode.OK, LocalizedResponseService.LocalizedResponseFactory.UserDeleted());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return _db.UserModels.Count(e => e.Id == id) > 0;
        }
    }
}