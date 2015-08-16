namespace ProCultura.Web.Api.Controllers
{
    using System.Web.Http.Description;

    using Procultura.Application.DTO;
    using Procultura.Application.DTO.User;
    using Procultura.Application.Services;

    using ProCultura.CrossCutting.Settings;

    using System.Web.Http;

    public class UserController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

    
        public ResponseBase PutUser(string token, UserModel request)
        {
            var response = _userAppService.UpdateUser(token, request);

            return response;
        }

        public UserModel GetUser(string token, int id)
        {
            var userModel = _userAppService.GetUser(token, id);

            return userModel;
        }

        public UserModel GetUser(string token)
        {
            var userModel = _userAppService.GetUser(token);

            return userModel;
        }

        public ResponseBase PostUser(RegisterModel request)
        {
            var response = _userAppService.CreateUser(request);

            return response;
        }

        private string GetLanguage(RequestBase request)
        {
            if (request.RequestInformation != null)
            {
                return AppStrings.ResourceManager.GetString(request.RequestInformation.LanguageId); ;
            }

            return AppStrings.EnglishCode;
        }

        public ResponseBase DeleteUser(string token, DeleteUserModel request)
        {
            var response = _userAppService.DeleteUser(token, request);

            return response;
        }
    }
}