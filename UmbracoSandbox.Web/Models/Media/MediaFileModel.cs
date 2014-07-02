namespace UmbracoSandbox.Web.Models
{
    using Zone.UmbracoMapper;

    public class MediaFileModel : BaseNodeViewModel
    {
        public int Size { get; set; }

        public string FileExtension { get; set; }

        public string DomainWithUrl { get; set; }
    }
}
