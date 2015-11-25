namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Models.Content;

    public interface IBlogPostPageHandler
    {
        BlogPostViewModel GetBlogPostPageModel(IPublishedContent currentPage, IMember currentMember);
    }
}
