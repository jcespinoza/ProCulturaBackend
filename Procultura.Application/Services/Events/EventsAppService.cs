using System;
using System.Collections.Generic;
using Procultura.Application.DTO.Events;
using Procultura.Application.Exceptions.Events;
using Procultura.Application.Extensions;
using ProCultura.Domain.Entities.Events;
using ProCultura.Domain.Repositories;

namespace Procultura.Application.Services.Events
{
    public class EventsAppService : IEventsAppService
    {
        private readonly IRepository<Event> _eventRepository;

        public EventsAppService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public EventModel GetEventWithId(int eventId)
        {
            var eventEntity = GetEventById(eventId);
            if (eventEntity == null) throw new EventNotFoundException();

            return eventEntity.ProjectAs<EventModel>();
        }

        public EventModel CreateEvent(NewEventModel request)
        {
            if(request == null) throw new ArgumentNullException("request");
            
            var existentEvent = GetEventById(request.EventId);
            if(existentEvent != null) throw new EventAlreadyExistsException();

            var eventEntity = request.ProjectAs<Event>();
            eventEntity.Status = "NEW";
            var createdEntity = _eventRepository.Insert(eventEntity);
            _eventRepository.UnitOfWork.SaveChanges();

            return createdEntity.ProjectAs<EventModel>();
        }

        public EventModel UpdateEvent(NewEventModel request)
        {
            if (request == null) throw new ArgumentNullException("request");
            var eventFound = GetEventById(request.EventId);
            if (eventFound == null) throw new EventNotFoundException();

            request.ReplaceValues(eventFound);

            var updatedEntity =_eventRepository.Update(eventFound);
            _eventRepository.UnitOfWork.SaveChanges();

            return updatedEntity.ProjectAs<EventModel>();
        }

        public EventModel DeleteEvent(int eventId)
        {
            var eventFound = GetEventById(eventId);
            if (eventFound == null) throw new EventNotFoundException();

            _eventRepository.Delete(eventFound);
            _eventRepository.UnitOfWork.SaveChanges();

            return eventFound.ProjectAs<EventModel>();
        }

        public IEnumerable<EventModel> GetAllEvents()
        {
            return _eventRepository.GetAllList().ProjectAs<EventModel>();
        }

        public EventModel UpdateEvent(int id, NewEventModel request)
        {
            request.EventId = id;
            return UpdateEvent(request);
        }

        private Event GetEventById(int eventId)
        {
            var eventEntity = _eventRepository.FirstOrDefault(e => e.EventId == eventId);
            return eventEntity;
        }
    }
}