namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class HomePageController : BaseController
    {
        public HomePageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult HomePage()
        {
            var vm = new HomePageViewModel();
            Mapper.Map(CurrentPage, vm, recursiveProperties: GetRecursiveProperties());
            vm.ImageUrl = GravatarHelper.GravatarHelper.CreateGravatarUrl("rpurcell@thisiszone.com", 80, "", null, false, false);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
