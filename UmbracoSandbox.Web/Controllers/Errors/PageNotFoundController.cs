namespace UmbracoSandbox.Web.Controllers.Errors
{
    using System.Web.Mvc;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Pages;
    using UmbracoSandbox.Web.Models.Errors;

    public class PageNotFoundController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion Fields

        #region Constructor

        public PageNotFoundController(ILoggingService logger, IPageHandler handler)
            : base(logger)
        {
            _handler = handler;
        }

        #endregion Constructor

        #region Action Methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult PageNotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            var vm = _handler.GetPageModel<PageNotFoundViewModel>(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        #endregion Action Methods
    }
}