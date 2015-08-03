namespace Procultura.Application.Exceptions
{
    using System;

    using ProCultura.CrossCutting.L10N;

    public class InvalidPasswordException: ArgumentException
    {
        public InvalidPasswordException(string message): base (message){}

        public InvalidPasswordException(): base(LocalizationKeys.message_InvalidPassword){}
        
    }
}
