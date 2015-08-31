using System.Web.Http;

namespace ProCultura.Web.Api.Controllers
{
    using Procultura.Application.DTO.Events;
    using Procultura.Application.Services.Events;

    public class EventController : ApiController
    {
        private readonly IEventsAppService _eventsAppService;

        public EventController(IEventsAppService eventsAppService)
        {
            this._eventsAppService = eventsAppService;
        }

        public EventModel GetEvent(int eventId)
        {
            return _eventsAppService.GetEventWithId(eventId);
        }

        public EventModel PostEvent(NewEventModel request)
        {
            return _eventsAppService.CreateEvent(request);
        }

        public EventModel PutEvent(NewEventModel request)
        {
            return _eventsAppService.UpdateEvent(request);
        }

        public EventModel DeleteEvent(int eventId)
        {
            return _eventsAppService.DeleteEvent(eventId);
        }
    }
}