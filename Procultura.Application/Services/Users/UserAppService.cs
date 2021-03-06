﻿namespace Procultura.Application.Services.Users
{
    using System;

    using Procultura.Application.DTO;
    using Procultura.Application.DTO.User;
    using Procultura.Application.Exceptions;
    using Procultura.Application.Exceptions.Users;
    using Procultura.Application.Extensions;

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

            this._authRequestFactory = authRequestFactory;
            this._l10nService = l10NService;
            this._userRepository = userRepository;
        }

        public AuthModel GetAuth(LoginModel request)
        {
            var user = this.GetUserByEmail(request.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (!PasswordEncryptionService.CheckPassword(user, request.Password))
            {
                throw new InvalidPasswordException();
            }

            return this.BuildSuccessAuthModel(user);
        }

        public ResponseBase DeleteUser(string token, DeleteUserModel request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new EmptyEmailException();
            }

            var user = this.GetUserByEmail(request.Email);
            var tokenModel = this._authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = this.GetUserByEmail(tokenModel.Email);

            if (user == null || requestSendingUser == null)
            {
                throw new UserNotFoundException();
            }

            if (requestSendingUser.Id != user.Id && requestSendingUser.IsAdmin())
            {
                throw new  NotEnoughPrivilegesException();
            }

            this._userRepository.Delete(user);
            this._userRepository.UnitOfWork.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_UserDeleted);
        }

        public UserModel GetUser(string token)
        {
            var tokenModel = this._authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var foundUser = this.GetUserByEmail(tokenModel.Email);

            if (foundUser != null)
            {
                return foundUser.ProjectAs<UserModel>();
            }

            throw new UserNotFoundException();
        }

        public UserModel GetUser(string token, int id)
        {
            var tokenModel = this._authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);

            var obtainedUserEntity = this._userRepository.Single(x => x.Id == id);
            var userModel = obtainedUserEntity.ProjectAs<UserModel>();

            var requestingUser = this.GetUserByEmail(tokenModel.Email);

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

            if (this.GetUserByEmail(request.Email) != null)
            {
                throw new EmailInUseException();
            }

            var newUser = request.ProjectAs<UserEntity>();

            PasswordEncryptionService.Encrypt(newUser);
            this._userRepository.Insert(newUser);
            this._userRepository.UnitOfWork.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_RegistrationSuccess);
        }

        public ResponseBase UpdateUser(string token, UpdateUserModel request)
        {   
            //TODO: add method to check whether the entity is valid and call that method here

            var tokenModel = this._authRequestFactory.BuildDecryptedRequest<UserTokenModel>(token);
            var requestSendingUser = this.GetUserByEmail(tokenModel.Email);
            var userToModify = this._userRepository.Single(x => x.Id == request.Id);

            if (requestSendingUser == null || userToModify == null)
            {
                throw new UserNotFoundException();
            }

            if (requestSendingUser.Id != request.Id && !requestSendingUser.IsAdmin())
            {
                throw new NotEnoughPrivilegesException();
            }

            request.ReplaceValues(userToModify);

            this._userRepository.Update(userToModify);
            this._userRepository.UnitOfWork.SaveChanges();

            return BuildGenericResponse(LocalizationKeys.message_UpdateUserSuccess);
        }

        private UserEntity GetUserByEmail(string email)
        {
            var user = this._userRepository.Single(x => x.Email == email);
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
