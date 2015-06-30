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