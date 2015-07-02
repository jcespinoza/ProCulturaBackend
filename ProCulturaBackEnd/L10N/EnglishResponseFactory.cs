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

        public string AuthRequestNotRecognizedMessage()
        {
            return "Couldn't validate permissions. Please log in again.";
        }

        public string InsufficientPrivilegesMessage()
        {
            return "You do not have sufficient privileges to perform this action.";
        }

        public string UserDeletedMessage()
        {
            return "User successfully deleted!";
        }

        public string UserNotFoundMessage()
        {
            return "User not found!";
        }

        public string UpdateUserSuccessMessage()
        {
            return "User successfully updated!";
        }

        public string InvalidPasswordMessage()
        {
            return "Invalid Password.";
        }
    }
}