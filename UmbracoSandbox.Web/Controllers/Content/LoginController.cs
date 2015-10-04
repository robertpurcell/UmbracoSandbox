namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using System.Web.Security;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Content;
    using UmbracoSandbox.Web.Models.Forms;

    public class LoginController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion Fields

        #region Constructor

        public LoginController(ILoggingService logger, IPageHandler handler)
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
        public ActionResult Login()
        {
            var vm = _handler.GetPageModel<LoginViewModel>(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Logs in the member
        /// </summary>
        /// <param name="vm">Login form view model</param>
        /// <returns>Redirect or return to current page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginMember(LoginForm vm)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            if (Members.Login(vm.Email, vm.Password))
            {
                FormsAuthentication.SetAuthCookie(vm.Email, vm.RememberMe);

                return RedirectToCurrentUmbracoUrl();
            }

            ModelState.AddModelError(string.Empty, "Login failed.");

            return CurrentUmbracoPage();
        }

        #endregion Action methods
    }
}
