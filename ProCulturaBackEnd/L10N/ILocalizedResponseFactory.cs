using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ProCulturaBackEnd.L10N
{
    public interface ILocalizedResponseFactory
    {
        string LoginSuccessMessage();
        string PasswordMismatchMessage();
        string EmailInUseMessage();
        string RegistrationSuccessMessage();
        string AuthRequestNotRecognizedMessage();
        string InsufficientPrivilegesMessage();
        string UserDeletedMessage();
        string UserNotFoundMessage();
        string UpdateUserSuccessMessage();
        string InvalidPasswordMessage();
    }
}