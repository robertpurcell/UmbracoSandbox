namespace UmbracoSandbox.Web.Handlers.Pages
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Pages;

    public interface IPageHandler
    {
        T GetPageModel<T>(IPublishedContent currentPage, IMember currentMember) where T : BasePageViewModel, new();
    }
}
