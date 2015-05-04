namespace UmbracoSandbox.Web.Handlers
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models;

    public interface INavigationHandler
    {
        NavigationModel GetMainNavigation(IPublishedContent currentPage);

        NavigationModel GetFooterNavigation(IPublishedContent currentPage);
    }
}
