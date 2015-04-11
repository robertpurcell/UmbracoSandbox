namespace UmbracoSandbox.Web.Models
{
    using Zone.UmbracoMapper;

    public class FileModel : BaseNodeViewModel
    {
        [PropertyMapping(SourceProperty = "umbracoBytes")]
        public int Size { get; set; }

        [PropertyMapping(SourceProperty = "umbracoExtension")]
        public string Extension { get; set; }
    }
}
