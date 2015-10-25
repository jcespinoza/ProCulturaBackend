using System;

namespace Procultura.Application.DTO.Events
{
    /// <summary>
    /// A DTO for the Event entity
    /// </summary>
    public class EventModel : ResponseBase
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
    }
}
