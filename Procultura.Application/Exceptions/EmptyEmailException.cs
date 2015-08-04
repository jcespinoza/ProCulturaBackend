namespace Procultura.Application.Exceptions
{
    using System;

    using ProCultura.CrossCutting.L10N;

    public class EmptyEmailException : ArgumentException
    {
        public EmptyEmailException(string message) : base(message) { }

        public EmptyEmailException() : base(LocalizationKeys.message_EmailIsRequired) { }

    }
}
