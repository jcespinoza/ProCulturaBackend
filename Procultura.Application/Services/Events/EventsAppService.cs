namespace Procultura.Application.Services.Events
{
    using System;

    using Procultura.Application.DTO.Events;
    using Procultura.Application.Exceptions.Events;
    using Procultura.Application.Extensions;

    using ProCultura.Domain.Entities.Events;
    using ProCultura.Domain.Repositories;

    public class EventsAppService : IEventsAppService
    {
        private readonly IRepository<Event> _eventRepository;

        public EventsAppService(IRepository<Event> eventRepository)
        {
            this._eventRepository = eventRepository;
        }

        public EventModel GetEventWithId(int eventId)
        {
            var eventEntity = this.GetEventById(eventId);
            if (eventEntity == null) throw new EventNotFoundException();

            return eventEntity.ProjectAs<EventModel>();
        }

        public EventModel CreateEvent(NewEventModel request)
        {
            if(request == null) throw new ArgumentNullException("request");
            
            var existentEvent = this.GetEventById(request.EventId);
            if(existentEvent != null) throw new EventAlreadyExistsException();

            var eventEntity = request.ProjectAs<Event>();
            var createdEntity = _eventRepository.Insert(eventEntity);

            return createdEntity.ProjectAs<EventModel>();
        }

        public EventModel UpdateEvent(NewEventModel request)
        {
            if (request == null) throw new ArgumentNullException("request");
            var eventFound = this.GetEventById(request.EventId);
            if (eventFound == null) throw new EventNotFoundException();

            request.ReplaceValues(eventFound);

            var updatedEntity =_eventRepository.Update(eventFound);
            return updatedEntity.ProjectAs<EventModel>();
        }

        public EventModel DeleteEvent(int eventId)
        {
            var eventFound = this.GetEventById(eventId);
            if (eventFound == null) throw new EventNotFoundException();

            _eventRepository.Delete(eventFound);
            return eventFound.ProjectAs<EventModel>();
        }

        private Event GetEventById(int eventId)
        {
            var eventEntity = this._eventRepository.FirstOrDefault(e => e.EventId == eventId);
            return eventEntity;
        }
    }
}