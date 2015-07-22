using System;

namespace ProCultura.Web.Api.L10N
{
    public class FrenchResponseFactory : ILocalizedResponseFactory
    {
        public string LoginSuccessMessage()
        {
            return "Connexion Réussie!";
        }

        public string PasswordMismatchMessage()
        {
            return "Les mots de passe ne correspondent pas!";
        }

        public string EmailInUseMessage()
        {
            return "Cet email est déjà utilisé!";
        }

        public string RegistrationSuccessMessage()
        {
            return "Inscription Réussi!";
        }

        public string AuthRequestNotRecognizedMessage()
        {
            throw new NotImplementedException();
        }

        public string InsufficientPrivilegesMessage()
        {
            throw new NotImplementedException();
        }

        public string UserDeletedMessage()
        {
            throw new NotImplementedException();
        }

        public string UserNotFoundMessage()
        {
            throw new NotImplementedException();
        }

        public string UpdateUserSuccessMessage()
        {
            throw new NotImplementedException();
        }

        public string InvalidPasswordMessage()
        {
            throw new NotImplementedException();
        }
    }
}