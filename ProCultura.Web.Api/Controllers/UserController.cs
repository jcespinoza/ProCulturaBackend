using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

using AutoMapper;

namespace ProCultura.Web.Api.Controllers
{
    using ProCultura.CrossCutting.Encryption;
    using ProCultura.Domain.Entities;
    using ProCultura.Domain.Services;
    using ProCultura.Localization;
    using ProCultura.Web.Api.Contexts;
    using ProCultura.Web.Api.Models;
    
    [EnableCors(origins: "http://localhost:8090", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        private readonly IAuthRequestFactory authRequestFactory;

        public UserController(IAuthRequestFactory _authRequestFactory)
        {
            //TODO: inject this dependency later
            authRequestFactory = new AuthRequestFactory(null);
        }

        // PUT api/user/5
        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PutUser(string token, UserModel recievedUser) 
        {
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            var user = Mapper.Map<UserEntity>(recievedUser);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tokenModel = authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);
            if (requestSendingUser == null)
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.AuthRequestNotRecognizedMessage());
            if (requestSendingUser.Id != user.Id && requestSendingUser.Role <= user.Role) //check for clearance
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.InsufficientPrivilegesMessage());
            var obtaineduser = _db.UserModels.FirstOrDefault(x => x.Id == user.Id);
            if (obtaineduser != null)
            {
                user.Password = obtaineduser.Password;
                user.Salt = obtaineduser.Salt;
                _db.Entry(obtaineduser).CurrentValues.SetValues(user);
            }
            else
                return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFoundMessage());
            _db.SaveChanges();
            var authModel = new AuthModel
            {
                Id = user.Id,
                AccessToken = authRequestFactory.BuildEncryptedRequest(user.Email),
                Mensaje = LocalizedResponseService.LocalizedResponseFactory.UpdateUserSuccessMessage()
            };
            return Ok(authModel);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token, int id)
        {
            var tokenModel = authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            var obtaineduserEntity = _db.UserModels.FirstOrDefault(x => x.Id == id);
            var obtaineduser = Mapper.Map<UserModel>(obtaineduserEntity);
            var requestingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);

            if (obtaineduser == null || requestingUser == null) return NotFound();

            if (obtaineduserEntity == requestingUser)
                return Ok(obtaineduser);

            if(requestingUser.Role >= obtaineduser.Role)
                return Ok(obtaineduser);

            return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFoundMessage());
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
                return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFoundMessage());
            var tokenModel = authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);
            if (requestSendingUser == null)
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.AuthRequestNotRecognizedMessage());
            if (requestSendingUser.Id != user.Id && requestSendingUser.Role <= user.Role) //check for clearance
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.InsufficientPrivilegesMessage());
            _db.UserModels.Remove(user);
            _db.SaveChanges();
            return new HttpActionResult(HttpStatusCode.OK, LocalizedResponseService.LocalizedResponseFactory.UserDeletedMessage());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}