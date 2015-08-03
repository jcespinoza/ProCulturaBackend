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

        public ResponseBase DeleteUser(DeleteUserModel request)
        {
            throw new System.NotImplementedException();
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
    }
}
