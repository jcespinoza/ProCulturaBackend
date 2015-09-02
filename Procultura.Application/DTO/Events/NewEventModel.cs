namespace Procultura.Application.DTO.Events
{
    using System;

    public class NewEventModel : RequestBase
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
    }
}
