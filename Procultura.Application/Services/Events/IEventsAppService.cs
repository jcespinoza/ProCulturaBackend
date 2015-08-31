namespace Procultura.Application.Services.Events
{
    using Procultura.Application.DTO.Events;

    /// <summary>
    /// Contract for Events application service implementations
    /// </summary>
    public interface IEventsAppService
    {
        /// <summary>
        /// Gets a single event with the given Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        EventModel GetEventWithId(int eventId);

        /// <summary>
        /// Creates a new Event and saves it to the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        EventModel CreateEvent(NewEventModel request);

        /// <summary>
        /// Update a event with the new values
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        EventModel UpdateEvent(NewEventModel request);

        /// <summary>
        /// Deletes the event with the given Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        EventModel DeleteEvent(int eventId);
    }
}
