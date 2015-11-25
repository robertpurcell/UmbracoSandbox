namespace UmbracoSandbox.Web.Handlers.Navigation
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Navigation;

    public interface INavigationHandler
    {
        MainNavigationViewModel GetMainNavigation(IPublishedContent currentPage, IMember currentMember);

        NavigationViewModel GetFooterNavigation(IPublishedContent currentPage);
    }
}
