namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Service.EmailService;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class HomeController : BaseController
    {
        #region Constructor

        public HomeController(IUmbracoMapper mapper, IEmailService mailer)
            : base(mapper, mailer)
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
            var vm = GetPageModel<HomeViewModel>();

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
