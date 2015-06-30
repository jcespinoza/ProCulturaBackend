using System;

namespace ProCulturaBackEnd.L10N
{
    public class SpanishResponseFactory : ILocalizedResponseFactory
    {
        public string LoginSuccessMessage()
        {
            return "Autenticacion Exitosa!";
        }

        public string PasswordMismatchMessage()
        {
            return "La contraseña no coincide.";
        }

        public string EmailInUseMessage()
        {
            return "Este correo se encuentra en uso.";
        }

        public string RegistrationSuccessMessage()
        {
            return "Registracion exitosa!";
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