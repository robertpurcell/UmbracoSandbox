namespace UmbracoSandbox.Web.Models.Media
{
    using UmbracoSandbox.Web.Infrastructure.Config;

    using Zone.UmbracoMapper;

    public class FileModel : BaseNodeViewModel
    {
        [PropertyMapping(SourceProperty = PropertyAliases.UmbracoBytes)]
        public int Size { get; set; }

        [PropertyMapping(SourceProperty = PropertyAliases.UmbracoExtension)]
        public string Extension { get; set; }
    }
}
