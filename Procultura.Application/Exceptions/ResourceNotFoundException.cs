namespace Procultura.Application.Exceptions
{
    using System;

    using ProCultura.CrossCutting.L10N;

    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }

        public ResourceNotFoundException() : base(LocalizationKeys.message_ResourceNotFound) { }

    }
}
