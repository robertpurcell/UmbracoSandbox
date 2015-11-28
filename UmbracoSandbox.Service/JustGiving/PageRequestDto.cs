namespace UmbracoSandbox.Service.JustGiving
{
    using System;
    using System.Collections.Generic;

    public class PageRequestDto
    {
        public int? EventId { get; set; }

        public DateTime? EventDate { get; set; }

        public string EventName { get; set; }

        public string ActivityType { get; set; }

        public int CharityId { get; set; }

        public bool CharityOptIn { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool CharityFunded { get; set; }

        public bool JustGivingOptIn { get; set; }

        public string PageShortName { get; set; }

        public string PageStory { get; set; }

        public string PageSummaryWhat { get; set; }

        public string PageSummaryWhy { get; set; }

        public string PageTitle { get; set; }

        public decimal? TargetAmount { get; set; }

        public IList<ImageInfo> Images { get; set; }

        public string Attribution { get; set; }

        public class ImageInfo
        {
            public string Caption { get; set; }

            public string Url { get; set; }

            public bool IsDefault { get; set; }
        }
    }
}
