namespace UmbracoSandbox.Web.Handlers.Modules
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Modules;

    public interface IMetadataHandler
    {
        MetadataViewModel GetMetadata(IPublishedContent currentPage);
    }
}
