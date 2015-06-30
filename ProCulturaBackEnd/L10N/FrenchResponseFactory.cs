﻿using System;

namespace ProCulturaBackEnd.L10N
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