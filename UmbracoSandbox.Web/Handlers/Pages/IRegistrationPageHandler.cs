namespace UmbracoSandbox.Web.Handlers.Pages
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Pages;

    public interface IRegistrationPageHandler
    {
        RegistrationViewModel GetRegistrationPageModel(IPublishedContent currentPage, IMember currentMember);
    }
}
