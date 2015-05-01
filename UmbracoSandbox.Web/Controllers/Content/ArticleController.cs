namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Handlers;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class ArticleController : BaseController
    {
        #region Constructor

        public ArticleController(IUmbracoMapper mapper, IPageHandler handler)
            : base(mapper, handler)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Article()
        {
            var vm = Handler.GetPageModel<ArticleViewModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
