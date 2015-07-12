namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Content;

    public class ArticleController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion

        #region Constructor

        public ArticleController(ILoggingService logger, IPageHandler handler)
            : base(logger)
        {
            _handler = handler;
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Article()
        {
            var vm = _handler.GetPageModel<ArticleViewModel>(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
