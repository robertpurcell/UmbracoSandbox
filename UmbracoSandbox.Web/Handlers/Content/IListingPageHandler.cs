namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Content;

    public interface IListingPageHandler
    {
        ListingViewModel GetListingPageModel<T>(IPublishedContent currentPage, bool isLoggedIn) where T : BaseModuleModel, new();
    }
}
