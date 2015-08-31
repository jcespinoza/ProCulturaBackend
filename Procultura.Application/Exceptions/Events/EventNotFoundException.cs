namespace Procultura.Application.Exceptions.Events
{
    using ProCultura.CrossCutting.L10N;

    public class EventNotFoundException : ResourceNotFoundException
    {
        public EventNotFoundException(string message) : base(message) { }

        public EventNotFoundException() : base(LocalizationKeys.message_EventNotFound) { }

    }
}
