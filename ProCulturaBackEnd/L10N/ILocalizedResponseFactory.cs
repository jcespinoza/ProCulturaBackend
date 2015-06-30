﻿using System;
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
        string AuthRequestNotRecognized();
        string InsufficientPrivileges();
        string UserDeleted();
        string UserNotFound();
        string UpdateUserSuccessMessage();
        string InvalidPassword();
    }
}