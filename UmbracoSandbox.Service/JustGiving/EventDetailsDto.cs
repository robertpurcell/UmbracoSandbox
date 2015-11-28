namespace UmbracoSandbox.Service.JustGiving
{
    using System;

    public class EventDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string EventType { get; set; }

        public string Location { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }
    }
}
