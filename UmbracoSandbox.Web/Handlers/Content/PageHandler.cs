namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    using UmbracoSandbox.Web.Handlers.Base;
    using UmbracoSandbox.Web.Handlers.Modules;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Models.Base;

    using Zone.UmbracoMapper;

    public class PageHandler : BaseHandler, IPageHandler
    {
        #region Fields

        private readonly IMetadataHandler _metadataHandler;
        private readonly INavigationHandler _navigationHandler;

        #endregion Fields

        #region Constructor

        public PageHandler(IUmbracoMapper mapper, IMetadataHandler metadataHandler, INavigationHandler navigationHandler)
            : base(mapper)
        {
            _metadataHandler = metadataHandler;
            _navigationHandler = navigationHandler;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the model for the page
        /// </summary>
        /// <typeparam name="T">The page type</typeparam>
        /// <param name="currentPage">The current page</param>
        /// <param name="currentMember">The current member</param>
        /// <returns>The page model</returns>
        public virtual T GetPageModel<T>(IPublishedContent currentPage, IMember currentMember) where T : BasePageViewModel, new()
        {
            var model = new T
            {
                Metadata = _metadataHandler.GetMetadata(currentPage),
                MainNavigation = _navigationHandler.GetMainNavigation(currentPage, currentMember),
                FooterNavigation = _navigationHandler.GetFooterNavigation(currentPage)
            };
            Mapper.Map(currentPage, model);

            return model;
        }

        #endregion Methods
    }
}
