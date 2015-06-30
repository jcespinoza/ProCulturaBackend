using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ProCulturaBackEnd.Contexts;
using ProCulturaBackEnd.L10N;
using ProCulturaBackEnd.Models;
using ProCulturaBackEnd.Services;

namespace ProCulturaBackEnd.Controllers
{
    public class LoginController : ApiController
    {
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        // POST api/Login2
        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PostUserModel(LoginModel usermodel)
        {
            var user = _db.UserModels.FirstOrDefault(x => x.Email == usermodel.Email);
            if (user == null)
                return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFound());
            if (!PasswordEncryptionService.CheckPassword(user, usermodel.Password))
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.InvalidPassword());
            var authModel = new AuthModel
            {
                AccessToken = AuthRequestFactory.BuildEncryptedRequest(user.Email),
                Mensaje = LocalizedResponseService.LocalizedResponseFactory.LoginSuccessMessage()
            };
            return Ok(authModel);
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