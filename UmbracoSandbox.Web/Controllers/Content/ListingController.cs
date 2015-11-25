namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Modules;

    public class ListingController : BaseController
    {
        #region Fields

        private readonly IListingPageHandler _handler;

        #endregion Fields

        #region Constructor

        public ListingController(ILoggingService logger, IListingPageHandler handler)
            : base(logger)
        {
            _handler = handler;
        }

        #endregion Constructor

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Listing()
        {
            var vm = _handler.GetListingPageModel<BlogPostModuleViewModel>(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        #endregion Action methods
    }
}
