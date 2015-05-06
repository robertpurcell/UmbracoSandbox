namespace UmbracoSandbox.Web.Handlers.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Handlers.Base;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Models.Base;
    using Zone.UmbracoMapper;

    public class PageHandler : BaseHandler, IPageHandler
    {
        #region Constructor

        public PageHandler(IUmbracoMapper mapper, INavigationHandler navigationHandler)
            : base(mapper)
        {
            NavigationHandler = navigationHandler;
        }

        #endregion

        #region Properties

        protected INavigationHandler NavigationHandler { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the model for the page
        /// </summary>
        /// <typeparam name="T">The page type</typeparam>
        /// <param name="currentPage">The current page</param>
        /// <returns>The page model</returns>
        public virtual T GetPageModel<T>(IPublishedContent currentPage) where T : BasePageViewModel, new()
        {
            var model = new T
            {
                AbsoluteUrl = currentPage.UrlAbsolute(),
                MainNavigation = NavigationHandler.GetMainNavigation(currentPage),
                FooterNavigation = NavigationHandler.GetFooterNavigation(currentPage)
            };
            Mapper.Map(currentPage, model);

            return model;
        }

        #endregion
    }
}
