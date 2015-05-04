namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Handlers;
    using UmbracoSandbox.Web.Models;

    public class HomeController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion

        #region Constructor

        public HomeController(IPageHandler handler)
        {
            _handler = handler;
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Home()
        {
            var vm = _handler.GetPageModel<HomeViewModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
