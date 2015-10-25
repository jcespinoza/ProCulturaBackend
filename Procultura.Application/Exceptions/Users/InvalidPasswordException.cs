using System;
using ProCultura.CrossCutting.L10N;

namespace Procultura.Application.Exceptions.Users
{
    public class InvalidPasswordException: ArgumentException
    {
        public InvalidPasswordException(string message): base (message){}

        public InvalidPasswordException(): base(LocalizationKeys.message_InvalidPassword){}
        
    }
}
