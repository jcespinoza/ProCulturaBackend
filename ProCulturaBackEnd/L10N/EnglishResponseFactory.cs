using System;

namespace ProCulturaBackEnd.L10N
{
    public class EnglishResponseFactory : ILocalizedResponseFactory
    {
        public string LoginSuccessMessage()
        {
            return "Login Successful!";
        }

        public string PasswordMismatchMessage()
        {
            return "Passwords do not match!";
        }

        public string EmailInUseMessage()
        {
            return "Email is already in use!";
        }

        public string RegistrationSuccessMessage()
        {
            return "Registration Successful!";
        }

        public string AuthRequestNotRecognized()
        {
            throw new NotImplementedException();
        }

        public string InsufficientPrivileges()
        {
            throw new NotImplementedException();
        }

        public string UserDeleted()
        {
            throw new NotImplementedException();
        }

        public string UserNotFound()
        {
            throw new NotImplementedException();
        }

        public string UpdateUserSuccessMessage()
        {
            throw new NotImplementedException();
        }

        public string InvalidPassword()
        {
            throw new NotImplementedException();
        }
    }
}