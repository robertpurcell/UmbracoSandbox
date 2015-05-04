namespace UmbracoSandbox.Web.Handlers
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models;

    public interface IListingPageHandler
    {
        ListingViewModel GetListingPageModel<T>(IPublishedContent currentPage) where T : BaseModuleModel, new();
    }
}
