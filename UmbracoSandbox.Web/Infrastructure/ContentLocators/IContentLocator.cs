namespace UmbracoSandbox.Web.Infrastructure.ContentLocators
{
    using Umbraco.Core.Models;

    public interface IContentLocator
    {
        IPublishedContent Find(IPublishedContent parent, string documentTypeAlias);

        IPublishedContent Find(int id);
    }
}