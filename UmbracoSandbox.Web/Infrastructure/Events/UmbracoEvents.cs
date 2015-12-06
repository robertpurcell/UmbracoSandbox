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
                    case ContentTypeAliases.ServerError:
                        if (content.Published)
                        {
                            PublishErrorPage(e, content);
                        }

                        break;
                }
            }
        }

        private static void PublishErrorPage(CancellableEventArgs e, IContent content)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);
            var published = helper.TypedContent(content.Id);
            if (published != null)
            {
                var publisher = NinjectWebCommon.Kernel.GetService<IPublishingService>();
                if (publisher.PublishWebFormsPage(published.UrlWithDomain(), content.ContentType.Alias))
                {
                    e.Messages.Add(new EventMessage("Success", "Static error page updated", EventMessageType.Info));
                    content.SetValue(PropertyAliases.StaticPage, string.Format("/{0}.aspx", content.ContentType.Alias));
                }
                else
                {
                    e.Messages.Add(new EventMessage("Error", "Failure updating static error page", EventMessageType.Error));
                }
            }
            else
            {
                e.Messages.Add(new EventMessage("Warning", "Publish again to update static error page", EventMessageType.Warning));
            }
        }
    }
}
