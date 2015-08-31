namespace Procultura.Application.DTO.Events
{
    using System;

    using Procultura.Application.DTO;

    /// <summary>
    /// A DTO for the Event entity
    /// </summary>
    public class EventModel : ResponseBase
    {
        /// <summary>
        /// The unique Id for the Event
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// The event name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A summary of what this event is about
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The Date on which this event will ocurr
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// The Location where the event will be hold
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The URL for this event's image
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
