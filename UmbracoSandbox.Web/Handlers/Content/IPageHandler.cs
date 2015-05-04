namespace UmbracoSandbox.Web.Handlers
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models;

    public interface IPageHandler
    {
        T GetPageModel<T>(IPublishedContent currentPage) where T : BasePageViewModel, new();
    }
}
