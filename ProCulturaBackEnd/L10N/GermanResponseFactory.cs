using System;

namespace ProCulturaBackEnd.L10N
{
    public class GermanResponseFactory : ILocalizedResponseFactory
    {

        public string LoginSuccessMessage()
        {
            return "Anmeldung erfolgreich!";
        }

        public string PasswordMismatchMessage()
        {
            return "Passwoerter stimmen nicht ueberein!";
        }

        public string EmailInUseMessage()
        {
            return "E-mail ist bereits Einsatz!";
        }

        public string RegistrationSuccessMessage()
        {
            return "Registrierung erfolgreich!";
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