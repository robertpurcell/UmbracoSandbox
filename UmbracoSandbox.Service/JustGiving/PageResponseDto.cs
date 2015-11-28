namespace UmbracoSandbox.Service.JustGiving
{
    public class PageResponseDto
    {
        public ErrorDto Error { get; set; }

        public NextDto Next { get; set; }

        public int PageId { get; set; }

        public string SignOnUrl { get; set; }

        public string[] Warnings { get; set; }
    }
}
