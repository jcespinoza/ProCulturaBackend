using System.Web.Http;
using System.Web.Http.Description;

namespace ProCultura.Web.Api.Controllers
{
    using Procultura.Application.DTO.User;
    using Procultura.Application.Services;

    public class LoginController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public LoginController(IUserAppService userAppService)
        {
            this._userAppService = userAppService;
        }

        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PostUserModel(LoginModel request)
        {
            var authModel = _userAppService.GetAuth(request);

            return Ok(authModel);
        }
    }
}