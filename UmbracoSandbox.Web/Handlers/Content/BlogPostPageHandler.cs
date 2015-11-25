namespace UmbracoSandbox.Web.Handlers.Content
{
    using GravatarHelper;

    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Handlers.Modules;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Models.Content;

    using Zone.UmbracoMapper;

    public class BlogPostPageHandler : PageHandler, IBlogPostPageHandler
    {
        #region Constructor

        public BlogPostPageHandler(IUmbracoMapper mapper, IMetadataHandler metadataHandler, INavigationHandler navigationHandler)
            : base(mapper, metadataHandler, navigationHandler)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the model for the blog post page
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="currentMember">The current member</param>
        /// <returns>The page model</returns>
        public BlogPostViewModel GetBlogPostPageModel(IPublishedContent currentPage, IMember currentMember)
        {
            var model = GetPageModel<BlogPostViewModel>(currentPage, currentMember);
            model.ImageUrl = GravatarHelper.CreateGravatarUrl(model.Author.Email, 200, string.Empty, null, null, null);

            return model;
        }

        #endregion Methods
    }
}
