namespace UmbracoSandbox.Web.Infrastructure.Events
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Umbraco.Core.Logging;
    using Umbraco.Web.Models.ContentEditing;
    using Umbraco.Web.UI;

    public class NotificationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (string.Equals(request.RequestUri.AbsolutePath.ToLower(), "/umbraco/backoffice/umbracoapi/content/postsave"))
            {
                return base.SendAsync(request, cancellationToken)
                    .ContinueWith(task =>
                    {
                        var response = task.Result;
                        try
                        {
                            var content = ((ObjectContent)response.Content).Value as ContentItemDisplay;
                            if (content != null && content.Notifications.Any())
                            {
                                foreach (var notification in content.Notifications.Where(x => string.Equals(x.Header, "Content published")))
                                {
                                    notification.Message = "Thanks for publishing!";
                                    notification.NotificationType = SpeechBubbleIcon.Info;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error<NotificationHandler>("Error changing custom publishing cancelled message: " + ex.Message, ex);
                        }

                        return response;
                    }, cancellationToken);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
