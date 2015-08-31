namespace ProCultura.Domain.Entities.Events
{
    using System;

    /// <summary>
    /// En object representing the Event entity
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
        /// The Location where the event will be hold
        /// </summary>
        public string Location { get; set; }
    }
}
