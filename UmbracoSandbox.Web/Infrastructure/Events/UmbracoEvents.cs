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
                            var helper = new UmbracoHelper(UmbracoContext.Current);
                            var published = helper.TypedContent(content.Id);
                            if (published != null)
                            {
                                var errorCode = published.GetPropertyValue<int>(PropertyAliases.ErrorCode);
                                var publisher = NinjectWebCommon.Kernel.GetService<IPublishingService>();
                                publisher.PublishErrorPage(published.Url, errorCode);
                                e.Messages.Add(new EventMessage("Error page updated", errorCode.ToString(), EventMessageType.Info));
                            }
                        }

                        break;
                }
            }
        }
    }
}
