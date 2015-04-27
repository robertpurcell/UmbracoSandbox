namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Service.EmailService;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class HomePageController : BaseController
    {
        #region Constructor

        public HomePageController(IUmbracoMapper mapper, IEmailService mailer)
            : base(mapper, mailer)
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

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
