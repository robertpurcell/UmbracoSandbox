namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class ListingPageController : BaseController
    {
        #region Constructor

        public ListingPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult ListingPage()
        {
            var vm = GetPageModel<ListingPageViewModel>();

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
