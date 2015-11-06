namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Handlers.Modules;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Content;

    using Zone.UmbracoMapper;

    public class ListingPageHandler : PageHandler, IListingPageHandler
    {
        #region Constructor

        public ListingPageHandler(IUmbracoMapper mapper, IMetadataHandler metadataHandler, INavigationHandler navigationHandler)
            : base(mapper, metadataHandler, navigationHandler)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the model for the page
        /// </summary>
        /// <typeparam name="T">The child page type</typeparam>
        /// <param name="currentPage">The current page</param>
        /// <param name="currentMember">The current member</param>
        /// <returns>The page model</returns>
        public ListingViewModel GetListingPageModel<T>(IPublishedContent currentPage, IMember currentMember) where T : BaseModuleModel, new()
        {
            var model = GetPageModel<ListingViewModel>(currentPage, currentMember);
            Mapper.MapCollection(currentPage.Children, model.Items);

            return model;
        }

        #endregion Methods
    }
}
