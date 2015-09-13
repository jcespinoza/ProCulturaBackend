namespace Procultura.Application.Services.Events
{
    using System.Collections.Generic;

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

        /// <summary>
        /// Gets all events
        /// </summary>
        /// <returns></returns>
        IEnumerable<EventModel> GetAllEvents();

        /// <summary>
        /// Updates the given event
        /// </summary>
        /// <param name="id">The unique identifier of the event to update</param>
        /// <param name="request">The data to be assigned to the event</param>
        /// <returns></returns>
        EventModel UpdateEvent(int id, NewEventModel request);
    }
}
