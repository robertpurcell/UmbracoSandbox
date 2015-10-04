namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;

    using GravatarHelper;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Content;

    public class BlogPostController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion Fields

        #region Constructor

        public BlogPostController(ILoggingService logger, IPageHandler handler)
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
            var vm = _handler.GetPageModel<BlogPostViewModel>(CurrentPage, CurrentMember);
            vm.ImageUrl = GravatarHelper.CreateGravatarUrl("rpurcell@thisiszone.com", 200, string.Empty, null, null, null);

            return CurrentTemplate(vm);
        }

        #endregion Action methods
    }
}
