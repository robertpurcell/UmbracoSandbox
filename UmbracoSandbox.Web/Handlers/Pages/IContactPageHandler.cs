namespace UmbracoSandbox.Web.Handlers.Pages
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Pages;

    public interface IContactPageHandler
    {
        ContactViewModel GetContactPageModel(IPublishedContent currentPage, IMember currentMember);
    }
}
