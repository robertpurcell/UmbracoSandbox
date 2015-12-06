namespace UmbracoSandbox.Web.Controllers.Errors
{
    using System.Web.Mvc;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Errors;

    public class ServerErrorController : BaseController
    {
        #region Fields

        private readonly IErrorHandler _handler;

        #endregion Fields

        #region Constructor

        public ServerErrorController(ILoggingService logger, IErrorHandler handler)
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
        public ActionResult ServerError()
        {
            var vm = _handler.GetError500Model(CurrentPage);

            return CurrentTemplate(vm);
        }

        #endregion Action Methods
    }
}