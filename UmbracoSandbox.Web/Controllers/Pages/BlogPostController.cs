namespace UmbracoSandbox.Web.Controllers.Pages
{
    using System.Web.Mvc;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Pages;

    public class BlogPostController : BaseController
    {
        #region Fields

        private readonly IBlogPostPageHandler _handler;

        #endregion Fields

        #region Constructor

        public BlogPostController(ILoggingService logger, IBlogPostPageHandler handler)
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
        public ActionResult BlogPost()
        {
            var vm = _handler.GetBlogPostPageModel(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        #endregion Action methods
    }
}
