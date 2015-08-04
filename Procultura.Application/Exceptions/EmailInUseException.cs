using System;

namespace Procultura.Application.Exceptions
{
    using ProCultura.CrossCutting.L10N;
    public class EmailInUseException : ArgumentException
    {
        public EmailInUseException(string message) : base(message) { }

        public EmailInUseException() : base(LocalizationKeys.message_EmailInUse) { }

    }
}
