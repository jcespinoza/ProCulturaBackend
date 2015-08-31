namespace Procultura.Application.DTO.Events
{
    using System;

    public class NewEventModel : ResponseBase
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime BeginDate { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
    }
}
