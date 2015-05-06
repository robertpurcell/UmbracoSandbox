namespace UmbracoSandbox.Web.Handlers.Navigation
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Navigation;

    public interface INavigationHandler
    {
        NavigationModel GetMainNavigation(IPublishedContent currentPage);

        NavigationModel GetFooterNavigation(IPublishedContent currentPage);
    }
}
