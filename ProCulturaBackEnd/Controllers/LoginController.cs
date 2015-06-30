using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ProCulturaBackEnd.Contexts;
using ProCulturaBackEnd.Models;
using ProCulturaBackEnd.Services;

namespace ProCulturaBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:8090", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        // POST api/Login2
        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PostUserModel(LoginModel usermodel)
        {
            var user = _db.UserModels.FirstOrDefault(x => x.Email == usermodel.Email);
            if (user == null)
                return new HttpActionResult(HttpStatusCode.NotFound, LocalizedResponseService.LocalizedResponseFactory.UserNotFoundMessage());
            if (!PasswordEncryptionService.CheckPassword(user, usermodel.Password))
                return new HttpActionResult(HttpStatusCode.Forbidden, LocalizedResponseService.LocalizedResponseFactory.InvalidPasswordMessage());
            var authModel = new AuthModel
            {
                Id = user.Id,
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