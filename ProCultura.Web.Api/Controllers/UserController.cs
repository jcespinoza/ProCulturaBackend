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

        // PUT api/user/5
        [ResponseType(typeof(ResponseBase))]
        public IHttpActionResult PutUser(string token, UserModel request)
        {
            var response = _userAppService.UpdateUser(token, request);

            return Ok(response);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token, int id)
        {
            var userModel = _userAppService.GetUser(token, id);

            return Ok(userModel);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token)
        {
            var userModel = _userAppService.GetUser(token);

            return Ok(userModel);
        }

        [ResponseType(typeof(ResponseBase))]
        public IHttpActionResult PostUser(RegisterModel request)
        {
            var response = _userAppService.CreateUser(request);

            return Ok(response);
        }

        private string GetLanguage(RequestBase request)
        {
            if (request.RequestInformation != null)
            {
                return AppStrings.ResourceManager.GetString(request.RequestInformation.LanguageId); ;
            }

            return AppStrings.EnglishCode;
        }

        [ResponseType(typeof(ResponseBase))]
        public IHttpActionResult DeleteUser(string token, DeleteUserModel request)
        {
            var response = _userAppService.DeleteUser(token, request);

            return Ok(response);
        }
    }
}