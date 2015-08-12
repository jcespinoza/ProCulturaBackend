using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProCultura.Web.Api.Controllers
{
    using System;

    using Procultura.Application.DTO.User;
    using Procultura.Application.Exceptions;
    using Procultura.Application.Services;

    using ProCultura.CrossCutting.L10N;
    using ProCultura.CrossCutting.Settings;

    public class LoginController : ApiController
    {
        private readonly IUserAppService _userAppService;

        private readonly ILocalizationService l10NService;

        public LoginController(ILocalizationService _l10nService, IUserAppService userAppService)
        {
            if (_l10nService == null) throw new ArgumentNullException("_l10nService");
            if (userAppService == null) throw new ArgumentNullException("userAppService");

            l10NService = _l10nService;
            this._userAppService = userAppService;
        }

        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PostUserModel(LoginModel request)
        {
            var authModel = _userAppService.GetAuth(request);

            return Ok(authModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}