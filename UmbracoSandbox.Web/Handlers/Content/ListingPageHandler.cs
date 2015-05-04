namespace UmbracoSandbox.Web.Handlers
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class ListingPageHandler : PageHandler, IListingPageHandler
    {
        #region Constructor

        public ListingPageHandler(IUmbracoMapper mapper, INavigationHandler navigationHandler)
            : base(mapper, navigationHandler)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the model for the page
        /// </summary>
        /// <typeparam name="T">The child page type</typeparam>
        /// <param name="currentPage">The current page</param>
        /// <returns>The page model</returns>
        public ListingViewModel GetListingPageModel<T>(IPublishedContent currentPage) where T : BaseModuleModel, new()
        {
            var model = GetPageModel<ListingViewModel>(currentPage);
            Mapper.MapCollection<T>(currentPage.Children, model.Items as IList<T>);

            return model;
        }

        #endregion
    }
}
