using System;
using ProCultura.CrossCutting.L10N;

namespace Procultura.Application.Exceptions.Users
{
    public class EmptyEmailException : ArgumentException
    {
        public EmptyEmailException(string message) : base(message) { }

        public EmptyEmailException() : base(LocalizationKeys.message_EmailIsRequired) { }

    }
}
