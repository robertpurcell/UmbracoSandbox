namespace UmbracoSandbox.Web.Handlers.Pages
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Pages;

    public interface IBlogPostPageHandler
    {
        BlogPostViewModel GetBlogPostPageModel(IPublishedContent currentPage, IMember currentMember);
    }
}
