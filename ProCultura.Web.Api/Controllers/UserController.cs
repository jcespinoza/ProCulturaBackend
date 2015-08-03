namespace ProCultura.Web.Api.Controllers
{
    using System.Web.Http.Description;

    using Procultura.Application.DTO;
    using Procultura.Application.DTO.User;
    using Procultura.Application.Exceptions;
    using Procultura.Application.Services;

    using ProCultura.CrossCutting.Encryption;
    using ProCultura.CrossCutting.Settings;
    using ProCultura.Data.Context;
    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Services;
    using ProCultura.CrossCutting.L10N;

    using System.Linq;
    using System.Net;
    using System.Web.Http;

    using AutoMapper;
    
    public class UserController : ApiController
    {
        //TODO: receive this as a dependency
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        private readonly IAuthRequestFactory _authRequestFactory;
        private readonly ILocalizationService _l10nService;

        private readonly IUserAppService _userAppService;

        public UserController(IAuthRequestFactory authRequestFactory, ILocalizationService l10nService, IUserAppService userAppService)
        {
            _authRequestFactory = authRequestFactory;
            _l10nService = l10nService;
            _userAppService = userAppService;
        }

        // PUT api/user/5
        [ResponseType(typeof(ResponseBase))]
        public IHttpActionResult PutUser(string token, UserModel request)
        {
            var userEntity = Mapper.Map<UserEntity>(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);

            if (requestSendingUser == null)
                return new HttpActionResult(
                    HttpStatusCode.Forbidden,
                    _l10nService.GetLocalizedString(
                        LocalizationKeys.message_AuthRequestNotRecognized,
                        AppStrings.EnglishCode));

            if (requestSendingUser.Id != userEntity.Id && !requestSendingUser.IsAdmin())
                return new HttpActionResult(
                    HttpStatusCode.Forbidden,
                    _l10nService.GetLocalizedString(
                        LocalizationKeys.message_InsufficientPrivileges,
                        AppStrings.EnglishCode));

            var obtaineduser = _db.UserModels.FirstOrDefault(x => x.Id == userEntity.Id);
            if (obtaineduser != null)
            {
                userEntity.Password = obtaineduser.Password;
                userEntity.Salt = obtaineduser.Salt;
                _db.Entry(obtaineduser).CurrentValues.SetValues(userEntity);
                _db.SaveChanges();
            }
            else
            {
                return new HttpActionResult(
                    HttpStatusCode.NotFound,
                    _l10nService.GetLocalizedString(LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode));
            }

            var response = new ResponseBase
                                {
                                    Message =
                                        _l10nService.GetLocalizedString(
                                            LocalizationKeys.message_UpdateUserSuccess,
                                            AppStrings.EnglishCode)
                                };
            return Ok(response);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token, int id)
        {
            var userModel = _userAppService.GetUser(token, id);

            if (userModel.Exception is UserNotFoundException)
            {
                return this.BuildErrorActionResult(HttpStatusCode.NotFound,
                    userModel.Message, AppStrings.EnglishCode);
            }

            if (userModel.Exception is NotEnoughPrivilegesException)
            {
                return this.BuildErrorActionResult(HttpStatusCode.Forbidden,
                    LocalizationKeys.message_InsufficientPrivileges, AppStrings.EnglishCode);
            }

            return Ok(userModel);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token)
        {
            var userModel = _userAppService.GetUser(token);

            if (userModel.Exception is UserNotFoundException)
            {
                return this.BuildErrorActionResult(HttpStatusCode.NotFound, LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode);
            }

            return Ok(userModel);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult PostUser(RegisterModel request)
        {
            if (request.Email == null)
            {
                return this.BuildErrorActionResult(
                    HttpStatusCode.Forbidden,
                    LocalizationKeys.message_EmailIsRequired,
                    GetLanguage(request));
            }
            
            if (_db.UserModels.FirstOrDefault(x => x.Email == request.Email) != null)
                return this.BuildErrorActionResult(
                    HttpStatusCode.InternalServerError,
                    LocalizationKeys.message_EmailInUse,
                    AppStrings.EnglishCode);
            
            var newUser = Mapper.Map<UserEntity>(request);

            PasswordEncryptionService.Encrypt(newUser);
            _db.UserModels.Add(newUser);
            _db.SaveChanges();

            return Ok(this.BuildGenericResponse( LocalizationKeys.message_RegistrationSuccess, GetLanguage(request)) );
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
            var user = _db.UserModels.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return this.BuildErrorActionResult(HttpStatusCode.NotFound, LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode);
            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);

            if (requestSendingUser == null)
                return this.BuildErrorActionResult(HttpStatusCode.Forbidden, LocalizationKeys.message_AuthRequestNotRecognized, AppStrings.EnglishCode);

            if (requestSendingUser.Id != user.Id && requestSendingUser.IsAdmin())
                return this.BuildErrorActionResult(HttpStatusCode.Forbidden, LocalizationKeys.message_InsufficientPrivileges, AppStrings.EnglishCode);

            _db.UserModels.Remove(user);
            _db.SaveChanges();

            return Ok(BuildGenericResponse( LocalizationKeys.message_UserDeleted, GetLanguage(request) ));
        }

        private ResponseBase BuildGenericResponse(string messageKey, string languageCode) 
        {
            return new ResponseBase()
                       {
                           Message = _l10nService.GetLocalizedString(messageKey, languageCode)
                       };
        }

        private HttpActionResult BuildErrorActionResult(HttpStatusCode code, string message, string language)
        {
            return new HttpActionResult(code, _l10nService.GetLocalizedString(message, language));
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