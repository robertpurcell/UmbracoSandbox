namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Handlers;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class HomeController : BaseController
    {
        #region Constructor

        public HomeController(IUmbracoMapper mapper, IPageHandler handler)
            : base(mapper, handler)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Home()
        {
            var vm = Handler.GetPageModel<HomeViewModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
