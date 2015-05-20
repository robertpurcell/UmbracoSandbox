namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using System.Web.Security;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Content;
    using UmbracoSandbox.Web.Models.Forms;

    public class LoginController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion

        #region Constructor

        public LoginController(IPageHandler handler)
        {
            _handler = handler;
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Login()
        {
            var vm = _handler.GetPageModel<LoginViewModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

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
                FormsAuthentication.SetAuthCookie(vm.Email, false);

                return RedirectToCurrentUmbracoUrl();
            }

            ModelState.AddModelError(string.Empty, "Login failed.");

            return CurrentUmbracoPage();
        }

        #endregion
    }
}
