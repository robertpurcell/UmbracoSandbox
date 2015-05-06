namespace UmbracoSandbox.Service.Publishing
{
    public interface IPublishingService
    {
        bool PublishErrorPage(string url, int errorCode);
    }
}
