namespace UmbracoSandbox.Web.Controllers.Pages
{
    using System.Web.Mvc;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Pages;
    using UmbracoSandbox.Web.Models.Pages;

    public class HomeController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion Fields

        #region Constructor

        public HomeController(ILoggingService logger, IPageHandler handler)
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
        public ActionResult Home()
        {
            var vm = _handler.GetPageModel<HomeViewModel>(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        #endregion Action methods
    }
}
