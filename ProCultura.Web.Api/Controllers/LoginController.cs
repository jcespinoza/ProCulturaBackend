using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ProCultura.Web.Api.Controllers
{
    using ProCultura.CrossCutting.Encryption;
    using ProCultura.CrossCutting.L10N;
    using ProCultura.CrossCutting.Settings;
    using ProCultura.Data.Context;
    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Services;
    using ProCultura.Web.Api.Models;

    [EnableCors(origins: "http://localhost:8090", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private readonly ProCulturaBackEndContext db = new ProCulturaBackEndContext();

        private readonly IAuthRequestFactory authRequestFactory;

        private readonly ILocalizationService l10nService;

        public LoginController() : this(null, null){}

        public LoginController(IAuthRequestFactory _authRequestFactory, ILocalizationService _l10nService)
        {
            //TODO: inject this dependencies later
            authRequestFactory = new AuthRequestFactory(null);
            l10nService = new DatabaseLocalizationService();
        }

        // POST api/Login2
        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PostUserModel(LoginModel usermodel)
        {
            var user = GetUserByEmail(usermodel);
            if (user == null)
                return new HttpActionResult(
                    HttpStatusCode.NotFound,
                    l10nService.GetLocalizedString(LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode));

            if (!PasswordEncryptionService.CheckPassword(user, usermodel.Password))
                return new HttpActionResult(
                    HttpStatusCode.Forbidden,
                    l10nService.GetLocalizedString(LocalizationKeys.message_InvalidPassword, AppStrings.EnglishCode));

            return Ok(BuildSuccessAuthModel(user));
        }

        private AuthModel BuildSuccessAuthModel(UserEntity user)
        {
            var tokenModel = new UserTokenModel() { Email = user.Email };
            var authModel = new AuthModel
                                {
                                    Id = user.Id,
                                    AccessToken = authRequestFactory.BuildEncryptedRequest(tokenModel),
                                    Mensaje =
                                        l10nService.GetLocalizedString(
                                            LocalizationKeys.message_LoginSuccess,
                                            AppStrings.EnglishCode)
                                };
            return authModel;
        }

        private UserEntity GetUserByEmail(LoginModel usermodel)
        {
            var user = this.db.UserModels.FirstOrDefault(x => x.Email == usermodel.Email);
            return user;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}