using System;
using ProCultura.CrossCutting.L10N;

namespace Procultura.Application.Exceptions
{
    public class DuplicateResourceException : ArgumentException
    {
        public DuplicateResourceException(string message) : base(message) { }

        public DuplicateResourceException() : base(LocalizationKeys.message_DuplicateResource) { }

    }
}
