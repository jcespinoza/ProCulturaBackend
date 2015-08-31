using System.Web.Http;

namespace ProCultura.Web.Api.Controllers
{
    using Procultura.Application.DTO.User;
    using Procultura.Application.Services.Users;

    public class LoginController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public LoginController(IUserAppService userAppService)
        {
            this._userAppService = userAppService;
        }

     
        public AuthModel PostUserModel(LoginModel request)
        {
            var authModel = _userAppService.GetAuth(request);
            return authModel;
        }
    }
}