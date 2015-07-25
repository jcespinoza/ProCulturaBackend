using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ProCultura.Web.Api.Controllers
{
    using ProCultura.CrossCutting.Encryption;
    using ProCultura.Domain.Entities;
    using ProCultura.Domain.Services;
    using ProCultura.Localization;
    using ProCultura.Web.Api.Contexts;
    using ProCultura.Web.Api.Models;

    [EnableCors(origins: "http://localhost:8090", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        private readonly IAuthRequestFactory authRequestFactory;

        public LoginController(IAuthRequestFactory _authRequestFactory)
        {
            //TODO: inject this dependency later
            authRequestFactory = new AuthRequestFactory(null);
        }

        // POST api/Login2
        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PostUserModel(LoginModel usermodel)
        {
            var user = GetUserByEmail(usermodel);
            if (user == null)
                return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFoundMessage());

            if (!PasswordEncryptionService.CheckPassword(user, usermodel.Password))
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.InvalidPasswordMessage());

            return Ok(BuildSuccessAuthModel(user));
        }

        private AuthModel BuildSuccessAuthModel(UserEntity user)
        {
            var tokenModel = new UserTokenModel() { Email = user.Email };
            var authModel = new AuthModel
                                {
                                    Id = user.Id,
                                    AccessToken = authRequestFactory.BuildEncryptedRequest(tokenModel),
                                    Mensaje = LocalizedResponseService.LocalizedResponseFactory.LoginSuccessMessage()
                                };
            return authModel;
        }

        private UserEntity GetUserByEmail(LoginModel usermodel)
        {
            var user = this._db.UserModels.FirstOrDefault(x => x.Email == usermodel.Email);
            return user;
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