namespace UmbracoSandbox.Web.Infrastructure.ContentLocators
{
    using Umbraco.Core.Models;

    public interface IRootContentLocator
    {
        IPublishedContent Find();
    }
}