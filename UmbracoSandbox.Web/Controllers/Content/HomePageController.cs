namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class HomePageController : BaseController
    {
        #region Constructor

        public HomePageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult HomePage()
        {
            var vm = GetPageModel<HomePageViewModel>();
            Mapper.Map(CurrentPage, vm);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
