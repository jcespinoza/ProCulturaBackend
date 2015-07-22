namespace ProCultura.Web.Api.L10N
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

        public string AuthRequestNotRecognizedMessage()
        {
            return "No se pudieron validar los permisos. Por favor realize la autenticacion nuevamente.";
        }

        public string InsufficientPrivilegesMessage()
        {
            return "No tiene suficientes privilegios para realizar esta operacion.";
        }

        public string UserDeletedMessage()
        {
            return "Usuario borrado exitosamente!";
        }

        public string UserNotFoundMessage()
        {
            return "No se pudo encontrar el usuario!";
        }

        public string UpdateUserSuccessMessage()
        {
            return "Usuario actualizado exitosamente!";
        }

        public string InvalidPasswordMessage()
        {
            return "Contra contraseña es invalida.";
        }
    }
}