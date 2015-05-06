namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Base;

    public interface IPageHandler
    {
        T GetPageModel<T>(IPublishedContent currentPage) where T : BasePageViewModel, new();
    }
}
