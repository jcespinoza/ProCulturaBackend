namespace ProCultura.Web.Api.Controllers
{
    using System.Web.Http.Description;

    using ProCultura.CrossCutting.Encryption;
    using ProCultura.CrossCutting.Settings;
    using ProCultura.Data.Context;
    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Services;
    using ProCultura.CrossCutting.L10N;
    using ProCultura.Web.Api.Models;

    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper;
    
    [EnableCors(origins: "http://localhost:8090", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        private readonly IAuthRequestFactory authRequestFactory;
        private readonly ILocalizationService l10nService;

        public UserController() : this(null, null) { }

        public UserController(IAuthRequestFactory _authRequestFactory, ILocalizationService _l10nService)
        {
            //TODO: inject this dependency later
            authRequestFactory = new AuthRequestFactory(null);
            l10nService = new DatabaseLocalizationService();
        }

        // PUT api/user/5
        [ResponseType(typeof(AuthModel))]
        public IHttpActionResult PutUser(string token, UserModel receivedUser) 
        {
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            var user = Mapper.Map<UserEntity>(receivedUser);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokenModel = authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);

            if (requestSendingUser == null)
                return new HttpActionResult(
                    HttpStatusCode.Forbidden,
                    l10nService.GetLocalizedString(
                        LocalizationKeys.message_AuthRequestNotRecognized,
                        AppStrings.EnglishCode));

            if (requestSendingUser.Id != user.Id && !requestSendingUser.IsAdmin())
                return new HttpActionResult(
                    HttpStatusCode.Forbidden,
                    l10nService.GetLocalizedString(
                        LocalizationKeys.message_InsufficientPrivileges,
                        AppStrings.EnglishCode));

            var obtaineduser = _db.UserModels.FirstOrDefault(x => x.Id == user.Id);
            if (obtaineduser != null)
            {
                user.Password = obtaineduser.Password;
                user.Salt = obtaineduser.Salt;
                _db.Entry(obtaineduser).CurrentValues.SetValues(user);
                _db.SaveChanges();
            }
            else
            {
                return new HttpActionResult(
                    HttpStatusCode.NotFound,
                    l10nService.GetLocalizedString(LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode));
            }

            var authModel = new AuthModel
                                {
                                    Id = user.Id,
                                    AccessToken = authRequestFactory.BuildEncryptedRequest(user.Email),
                                    Mensaje =
                                        l10nService.GetLocalizedString(
                                            LocalizationKeys.message_UpdateUserSuccess,
                                            AppStrings.EnglishCode)
                                };
            return Ok(authModel);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token, int id)
        {
            var tokenModel = authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            var obtaineduserEntity = _db.UserModels.FirstOrDefault(x => x.Id == id);
            var obtaineduser = Mapper.Map<UserModel>(obtaineduserEntity);
            var requestingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);

            if (obtaineduserEntity == null || requestingUser == null) return NotFound();

            if (obtaineduserEntity == requestingUser)
                return Ok(obtaineduser);

            if(requestingUser.HasHigherAuthorityThan(obtaineduserEntity))
                return Ok(obtaineduser);

            return BuildActionResult(HttpStatusCode.NotFound, LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(string token)
        {
            var tokenModel = authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var foundUser = _db.UserModels.FirstOrDefault(u => u.Email == tokenModel.Email);

            if (foundUser != null)
            {
                Mapper.CreateMap<UserEntity, UserModel>();
                var userModel = Mapper.Map<UserModel>(foundUser);

                return Ok(userModel);
            }

            return BuildActionResult(HttpStatusCode.NotFound, LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode);
        }

        // POST api/user
        public IHttpActionResult PostUser(RegisterModel user)
        {
            if (_db.UserModels.FirstOrDefault(x => x.Email == user.Email) != null)
                return BuildActionResult(
                    HttpStatusCode.InternalServerError,
                    LocalizationKeys.message_EmailInUse,
                    AppStrings.EnglishCode);

            if (!user.Password.Equals(user.ConfirmPassword))
                return BuildActionResult(
                    HttpStatusCode.InternalServerError,
                    LocalizationKeys.message_PasswordMismatch,
                    AppStrings.EnglishCode);
            
            Mapper.CreateMap<RegisterModel, UserEntity>().ReverseMap();
            var newUser = Mapper.Map<UserEntity>(user);

            PasswordEncryptionService.Encrypt(newUser);
            _db.UserModels.Add(newUser);
            _db.SaveChanges();

            return BuildActionResult(HttpStatusCode.OK, LocalizationKeys.message_RegistrationSuccess, AppStrings.EnglishCode);
        }

        // DELETE api/user/5
        public IHttpActionResult DeleteUser(string token, int id)
        {
            var user = _db.UserModels.Find(id);
            if (user == null)
                return BuildActionResult(HttpStatusCode.NotFound, LocalizationKeys.message_UserNotFound, AppStrings.EnglishCode);
            var tokenModel = authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = _db.UserModels.FirstOrDefault(x => x.Email == tokenModel.Email);

            if (requestSendingUser == null)
                return BuildActionResult(HttpStatusCode.Forbidden, LocalizationKeys.message_AuthRequestNotRecognized, AppStrings.EnglishCode);

            if (requestSendingUser.Id != user.Id && requestSendingUser.IsAdmin())
                return BuildActionResult(HttpStatusCode.Forbidden, LocalizationKeys.message_InsufficientPrivileges, AppStrings.EnglishCode);

            _db.UserModels.Remove(user);
            _db.SaveChanges();
            return BuildActionResult(HttpStatusCode.OK, LocalizationKeys.message_UserDeleted, AppStrings.EnglishCode);
        }

        private HttpActionResult BuildActionResult(HttpStatusCode code, string message, string language)
        {
            return new HttpActionResult(code, l10nService.GetLocalizedString(message, language));
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