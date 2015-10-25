using System;
using ProCultura.CrossCutting.L10N;

namespace Procultura.Application.Exceptions.Users
{
    public class EmailInUseException : ArgumentException
    {
        public EmailInUseException(string message) : base(message) { }

        public EmailInUseException() : base(LocalizationKeys.message_EmailInUse) { }

    }
}
