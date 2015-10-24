namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Content;

    public interface IContactPageHandler
    {
        ContactViewModel GetContactPageModel(IPublishedContent currentPage, IMember currentMember);
    }
}
