namespace Procultura.Application.Services
{
    using System;
    using System.Linq;

    using Procultura.Application.DTO;
    using Procultura.Application.DTO.User;
    using Procultura.Application.Exceptions;
    using Procultura.Application.Extensions;

    using ProCultura.CrossCutting.Encryption;
    using ProCultura.CrossCutting.L10N;
    using ProCultura.Data.Context;
    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Services;

    public class UserAppService: IUserAppService
    {
        //TODO: receive this as a dependency
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        private readonly IAuthRequestFactory _authRequestFactory;

        private readonly ILocalizationService _l10nService;

        public UserAppService(IAuthRequestFactory authRequestFactory, ILocalizationService l10NService)
        {
            if (authRequestFactory == null) throw new ArgumentNullException("authRequestFactory");
            if (l10NService == null) throw new ArgumentNullException("l10NService");

            _authRequestFactory = authRequestFactory;
            _l10nService = l10NService;
        }

        public AuthModel GetAuth(LoginModel request)
        {
            var user = GetUserByEmail(request.Email);
            if (user == null)
            {
                return new AuthModel().MarkedWithException<AuthModel,UserNotFoundException>();
            }
            if (!PasswordEncryptionService.CheckPassword(user, request.Password))
            {
                return new AuthModel().MarkedWithException<AuthModel, InvalidPasswordException>();
            }

            return BuildSuccessAuthModel(user, request);
        }

        public ResponseBase DeleteUser(string token, DeleteUserModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return new ResponseBase().MarkedWithException<ResponseBase, EmptyEmailException>();
            }

            var user = GetUserByEmail(request.Email);
            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = GetUserByEmail(tokenModel.Email);

            if (user == null || requestSendingUser == null)
            {
                return new UserModel().MarkedWithException<UserModel, UserNotFoundException>();
            }

            if (requestSendingUser.Id != user.Id && requestSendingUser.IsAdmin())
            {
                return new ResponseBase().MarkedWithException<ResponseBase, NotEnoughPrivilegesException>();
            }

            _db.UserModels.Remove(user);
            _db.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_UserDeleted, request.GetRequestLanguage());
        }

        public UserModel GetUser(string token)
        {
            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var foundUser = this.GetUserByEmail(tokenModel.Email);

            if (foundUser != null)
            {
                return foundUser.ProjectAs<UserModel>();
            }

            return new UserModel().MarkedWithException<UserModel, UserNotFoundException>();
        }

        public UserModel GetUser(string token, int id)
        {
            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);

            var obtainedUserEntity = _db.UserModels.FirstOrDefault(x => x.Id == id);
            var userModel = obtainedUserEntity.ProjectAs<UserModel>();

            var requestingUser = this.GetUserByEmail(tokenModel.Email);

            if (obtainedUserEntity == null && requestingUser == null)
            {
                return new UserModel().MarkedWithException<UserModel, UserNotFoundException>();
            }

            if (obtainedUserEntity == requestingUser || requestingUser.HasHigherAuthorityThan(obtainedUserEntity))
            {
                return userModel;
            }

            return new UserModel().MarkedWithException<UserModel, NotEnoughPrivilegesException>();
        }

        public ResponseBase CreateUser(RegisterModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return new ResponseBase().MarkedWithException<ResponseBase, EmptyEmailException>();
            }

            if (GetUserByEmail(request.Email) != null)
            {
                return new ResponseBase().MarkedWithException<ResponseBase, EmailInUseException>();
            }

            var newUser = request.ProjectAs<UserEntity>();

            PasswordEncryptionService.Encrypt(newUser);
            _db.UserModels.Add(newUser);
            _db.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_RegistrationSuccess, request.GetRequestLanguage());
        }

        private UserEntity GetUserByEmail(string email)
        {
            var user = _db.UserModels.FirstOrDefault(x => x.Email == email);
            return user;
        }

        private AuthModel BuildSuccessAuthModel(UserEntity user, LoginModel request)
        {
            var tokenModel = new UserTokenModel { Email = user.Email };
            var authModel = new AuthModel
            {
                Id = user.Id,
                AccessToken = _authRequestFactory.BuildEncryptedRequest(tokenModel),
                Message =
                    _l10nService.GetLocalizedString(
                        LocalizationKeys.message_LoginSuccess,
                        request.GetRequestLanguage())
            };
            return authModel;
        }

        private ResponseBase BuildGenericResponse(string messageKey, string languageId)
        {
            return new ResponseBase()
            {
                Message = _l10nService.GetLocalizedString(messageKey, languageId)
            };
        }
    }
}
