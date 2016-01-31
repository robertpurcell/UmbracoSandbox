namespace UmbracoSandbox.Web.Handlers.Pages
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Modules;
    using UmbracoSandbox.Web.Models.Pages;

    public interface IListingPageHandler
    {
        ListingViewModel GetListingPageModel<T>(IPublishedContent currentPage, IMember currentMember) where T : BaseModuleViewModel, new();
    }
}
