using ProCultura.CrossCutting.L10N;

namespace Procultura.Application.Exceptions.Events
{
    public class EventNotFoundException : ResourceNotFoundException
    {
        public EventNotFoundException(string message) : base(message) { }

        public EventNotFoundException() : base(LocalizationKeys.message_EventNotFound) { }

    }
}
