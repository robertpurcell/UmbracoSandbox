namespace UmbracoSandbox.Service.EmailService
{
    using System.Collections.Generic;

    public class EmailDetail
    {
        public IList<string> To { get; set; }

        public IList<string> Bcc { get; set; }

        public string From { get; set; }

        public string DisplayName { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public IList<string> Attachments { get; set; }
    }
}
