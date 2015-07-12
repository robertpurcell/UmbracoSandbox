namespace UmbracoSandbox.Web.Handlers.Navigation
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Navigation;

    public interface INavigationHandler
    {
        MainNavigationModel GetMainNavigation(IPublishedContent currentPage, IPublishedContent currentMember);

        NavigationModel GetFooterNavigation(IPublishedContent currentPage);
    }
}
