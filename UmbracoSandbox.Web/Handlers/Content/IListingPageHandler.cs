namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Content;

    public interface IListingPageHandler
    {
        ListingViewModel GetListingPageModel<T>(IPublishedContent currentPage, IMember currentMember) where T : BaseModuleModel, new();
    }
}
