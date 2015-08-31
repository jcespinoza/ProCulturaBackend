namespace ProCultura.Domain.Entities.Events
{
    using System;

    /// <summary>
    /// An object representing the Event entity
    /// </summary>
    public class Event : IEntity
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
        /// The Date on which this event will end
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The Location where the event will be hold
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Price of the event´s entrance
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Full description of what`s the event about
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The URL for this event's image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Current status of the event
        /// </summary>
        public string Status { get; set; }
    }
}
