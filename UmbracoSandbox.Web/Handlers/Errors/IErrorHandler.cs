namespace UmbracoSandbox.Web.Handlers.Errors
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Errors;

    public interface IErrorHandler
    {
        ServerErrorViewModel GetError500Model(IPublishedContent currentPage);
    }
}
