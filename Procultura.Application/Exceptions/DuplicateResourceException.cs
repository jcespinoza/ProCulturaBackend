namespace Procultura.Application.Exceptions
{
    using System;

    using ProCultura.CrossCutting.L10N;

    public class DuplicateResourceException : ArgumentException
    {
        public DuplicateResourceException(string message) : base(message) { }

        public DuplicateResourceException() : base(LocalizationKeys.message_DuplicateResource) { }

    }
}
