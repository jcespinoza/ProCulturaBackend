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
    }
}