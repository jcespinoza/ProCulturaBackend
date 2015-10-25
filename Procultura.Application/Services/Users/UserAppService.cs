namespace Procultura.Application.Services.Users
{
    using System;

    using DTO;
    using DTO.User;
    using Exceptions;
    using Exceptions.Users;
    using Extensions;

    using ProCultura.CrossCutting.Encryption;
    using ProCultura.CrossCutting.L10N;
    using ProCultura.CrossCutting.Settings;
    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Repositories;
    using ProCultura.Domain.Services;

    public class UserAppService: IUserAppService
    {
        private readonly IAuthRequestFactory _authRequestFactory;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly ILocalizationService _l10nService;

        public UserAppService(IAuthRequestFactory authRequestFactory, ILocalizationService l10NService, IRepository<UserEntity> userRepository)
        {
            if (authRequestFactory == null) throw new ArgumentNullException("authRequestFactory");
            if (l10NService == null) throw new ArgumentNullException("l10NService");
            if (userRepository == null) throw new ArgumentNullException("userRepository");

            _authRequestFactory = authRequestFactory;
            _l10nService = l10NService;
            _userRepository = userRepository;
        }

        public AuthModel GetAuth(LoginModel request)
        {
            var user = GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (!PasswordEncryptionService.CheckPassword(user, request.Password))
            {
                throw new InvalidPasswordException();
            }

            return BuildSuccessAuthModel(user);
        }

        public ResponseBase DeleteUser(string token, DeleteUserModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new EmptyEmailException();
            }

            var user = GetUserByEmail(request.Email);
            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = GetUserByEmail(tokenModel.Email);

            if (user == null || requestSendingUser == null)
            {
                throw new UserNotFoundException();
            }

            if (requestSendingUser.Id != user.Id && requestSendingUser.IsAdmin())
            {
                throw new  NotEnoughPrivilegesException();
            }

            _userRepository.Delete(user);
            _userRepository.UnitOfWork.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_UserDeleted);
        }

        public UserModel GetUser(string token)
        {
            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var foundUser = GetUserByEmail(tokenModel.Email);

            if (foundUser != null)
            {
                return foundUser.ProjectAs<UserModel>();
            }

            throw new UserNotFoundException();
        }

        public UserModel GetUser(string token, int id)
        {
            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);

            var obtainedUserEntity = _userRepository.Single(x => x.Id == id);
            var userModel = obtainedUserEntity.ProjectAs<UserModel>();

            var requestingUser = GetUserByEmail(tokenModel.Email);

            if (obtainedUserEntity == null && requestingUser == null)
            {
                throw new UserNotFoundException();
            }

            if (obtainedUserEntity == requestingUser || requestingUser.HasHigherAuthorityThan(obtainedUserEntity))
            {
                return userModel;
            }

            throw new NotEnoughPrivilegesException();
        }

        public ResponseBase CreateUser(RegisterModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new EmptyEmailException();
            }

            if (GetUserByEmail(request.Email) != null)
            {
                throw new EmailInUseException();
            }

            var newUser = request.ProjectAs<UserEntity>();

            PasswordEncryptionService.Encrypt(newUser);
            _userRepository.Insert(newUser);
            _userRepository.UnitOfWork.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_RegistrationSuccess);
        }

        public ResponseBase UpdateUser(string token, UpdateUserModel request)
        {   
            //TODO: add method to check whether the entity is valid and call that method here

            var tokenModel = _authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = GetUserByEmail(tokenModel.Email);
            var userToModify = _userRepository.Single(x => x.Id == request.Id);

            if (requestSendingUser == null || userToModify == null)
            {
                throw new UserNotFoundException();
            }

            if (requestSendingUser.Id != request.Id && !requestSendingUser.IsAdmin())
            {
                throw new NotEnoughPrivilegesException();
            }

            request.ReplaceValues(userToModify);

            _userRepository.Update(userToModify);
            _userRepository.UnitOfWork.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_UpdateUserSuccess);
        }

        private UserEntity GetUserByEmail(string email)
        {
            var user = _userRepository.Single(x => x.Email == email);
            return user;
        }

        private AuthModel BuildSuccessAuthModel(UserEntity user)
        {
            var tokenModel = new UserTokenModel { Email = user.Email };
            var authModel = new AuthModel
            {
                Id = user.Id,
                AccessToken = _authRequestFactory.BuildEncryptedRequest(tokenModel),
                Message = LocalizationKeys.message_LoginSuccess
            };
            return authModel;
        }

        private ResponseBase BuildGenericResponse(string messageKey)
        {
            return new ResponseBase()
            {
                Message = messageKey
            };
        }
    }
}
