namespace Procultura.Application.Services.Events
{
    using Procultura.Application.DTO.Events;

    public interface IEventAppService
    {
        EventModel GetEventWithId(int eventId);
        EventModel CreateEvent(NewEventModel request);

        EventModel UpdateEvent(NewEventModel request);

        EventModel DeleteEvent(int eventId);
    }
}
