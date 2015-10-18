using System.Web.Http;

namespace ProCultura.WebApiOwin.Controllers
{
    using System.Collections.Generic;

    using Procultura.Application.DTO.Events;
    using Procultura.Application.Services.Events;

    public class EventController : ApiController
    {
        private readonly IEventsAppService _eventsAppService;

        public EventController(IEventsAppService eventsAppService)
        {
            this._eventsAppService = eventsAppService;
        }

        public IEnumerable<EventModel> GetEvents()
        {
            return _eventsAppService.GetAllEvents();
        }

        public EventModel GetEvent(int id)
        {
            return _eventsAppService.GetEventWithId(id);
        }

        public EventModel PostEvent(NewEventModel request)
        {
            return _eventsAppService.CreateEvent(request);
        }

        public EventModel PutEvent(NewEventModel request)
        {
            return _eventsAppService.UpdateEvent(request);
        }

        public EventModel PutEvent([FromUri] int id, [FromBody] NewEventModel request)
        {
            return _eventsAppService.UpdateEvent(id, request);
        }

        public EventModel DeleteEvent(int id)
        {
            return _eventsAppService.DeleteEvent(id);
        }
    }
}