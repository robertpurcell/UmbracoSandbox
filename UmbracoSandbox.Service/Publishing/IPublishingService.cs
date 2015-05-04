namespace UmbracoSandbox.Service.PublishingService
{
    public interface IPublishingService
    {
        bool PublishErrorPage(string url, int errorCode);
    }
}
