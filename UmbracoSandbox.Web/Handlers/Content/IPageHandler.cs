namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Base;

    public interface IPageHandler
    {
        T GetPageModel<T>(IPublishedContent currentPage, IPublishedContent currentMember) where T : BasePageViewModel, new();
    }
}
