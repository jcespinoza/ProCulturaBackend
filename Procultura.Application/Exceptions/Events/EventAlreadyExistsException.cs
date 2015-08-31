namespace Procultura.Application.Exceptions.Events
{
    using System;

    using ProCultura.CrossCutting.L10N;
    
    public class EventAlreadyExistsException : ArgumentException
    {
        public EventAlreadyExistsException(string message) : base(message) { }

        public EventAlreadyExistsException() : base(LocalizationKeys.message_EventAlreadyExist) { }
    }
}
