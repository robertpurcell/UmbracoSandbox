namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Content;

    public interface IRegistrationPageHandler
    {
        RegistrationViewModel GetRegistrationPageModel(IPublishedContent currentPage, IPublishedContent currentMember);
    }
}
