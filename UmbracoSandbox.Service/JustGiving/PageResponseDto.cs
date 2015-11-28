namespace UmbracoSandbox.Service.JustGiving
{
    public class PageResponseDto
    {
        public ErrorDto Error { get; set; }

        public NextDto Next { get; set; }

        public int PageId { get; set; }

        public string SignOnUrl { get; set; }

        public string[] Warnings { get; set; }

        public class ErrorDto
        {
            public string Id { get; set; }

            public string Desc { get; set; }
        }

        public class NextDto
        {
            public string Rel { get; set; }

            public string Uri { get; set; }

            public string Type { get; set; }
        }
    }
}
