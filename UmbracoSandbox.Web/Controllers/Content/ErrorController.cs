namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Handlers;
    using UmbracoSandbox.Web.Models;

    public class ErrorController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion

        #region Constructor

        public ErrorController(IPageHandler handler)
        {
            _handler = handler;
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Error()
        {
            var vm = _handler.GetPageModel<ErrorViewModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
