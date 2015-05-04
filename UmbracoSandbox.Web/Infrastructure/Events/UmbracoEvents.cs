namespace UmbracoSandbox.Web.Infrastructure.Events
{
    using Umbraco.Core.Events;
    using Umbraco.Core.Models;
    using Umbraco.Core.Publishing;
    using Umbraco.Web;
    using UmbracoSandbox.Service.PublishingService;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Infrastructure.Config;

    public static class UmbracoEvents
    {
        public static void Publishing(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            foreach (var content in e.PublishedEntities)
            {
                switch (content.ContentType.Alias)
                {
                    case PageTypes.Error:
                        var helper = new UmbracoHelper(UmbracoContext.Current);
                        var published = helper.TypedContent(content.Id);
                        var publisher = NinjectWebCommon.Kernel.GetService<IPublishingService>();
                        var test = published.GetPropertyValue<string>("errorCode");
                        int errorCode;
                        if (int.TryParse(published.GetPropertyValue<string>("errorCode"), out errorCode))
                        {
                            publisher.PublishErrorPage(published.Url, errorCode);
                        }

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
