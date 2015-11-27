namespace UmbracoSandbox.Web.Infrastructure.Events
{
    using Umbraco.Core.Events;
    using Umbraco.Core.Models;
    using Umbraco.Core.Publishing;
    using Umbraco.Web;

    using UmbracoSandbox.Service.Publishing;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Infrastructure.Config;

    public static class UmbracoEvents
    {
        public static void Published(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            foreach (var content in e.PublishedEntities)
            {
                switch (content.ContentType.Alias)
                {
                    case ContentTypeAliases.Error:
                        if (content.Published)
                        {
                            PublishErrorPage(e, content);
                        }

                        break;
                }
            }
        }

        private static void PublishErrorPage(PublishEventArgs<IContent> e, IContent content)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);
            var published = helper.TypedContent(content.Id);
            if (published != null)
            {
                var errorCode = published.GetPropertyValue<int>(PropertyAliases.ErrorCode);
                var publisher = NinjectWebCommon.Kernel.GetService<IPublishingService>();
                e.Messages.Add(publisher.PublishErrorPage(published.UrlWithDomain(), errorCode)
                    ? new EventMessage("Static error page updated", errorCode.ToString(), EventMessageType.Info)
                    : new EventMessage("Failure updating static error page", errorCode.ToString(), EventMessageType.Error));
            }
            else
            {
                e.Messages.Add(new EventMessage("Warning", "Publish again to update static error page", EventMessageType.Warning));
            }
        }
    }
}
