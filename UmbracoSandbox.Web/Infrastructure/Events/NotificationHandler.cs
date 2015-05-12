namespace UmbracoSandbox.Web.Infrastructure.Events
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Umbraco.Core.Logging;
    using Umbraco.Web.Models.ContentEditing;

    public class NotificationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.ToLower() == "/umbraco/backoffice/umbracoapi/content/postsave")
            {
                return base.SendAsync(request, cancellationToken)
                    .ContinueWith(task =>
                    {
                        var response = task.Result;
                        try
                        {
                            var data = response.Content;
                            var content = ((ObjectContent)data).Value as ContentItemDisplay;
                            if (content != null && content.Notifications.Count > 0)
                            {
                                foreach (var notification in content.Notifications)
                                {
                                    if (notification.Header.Equals("Content published"))
                                    {
                                        notification.Message = "Thanks for publishing!";
                                        notification.NotificationType = Umbraco.Web.UI.SpeechBubbleIcon.Error;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error<NotificationHandler>("Error changing custom publishing cancelled message: " + ex.InnerException, ex);
                        }

                        return response;
                    }, cancellationToken);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
