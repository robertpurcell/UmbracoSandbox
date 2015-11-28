namespace UmbracoSandbox.Service.JustGiving
{
    using System;

    public class EventRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string EventType { get; set; }

        public int CharityId { get; set; }

        public bool CharityOptIn { get; set; }

    }
}
