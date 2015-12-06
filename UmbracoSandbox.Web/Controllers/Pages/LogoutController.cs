namespace UmbracoSandbox.Web.Controllers.Pages
{
    using System.Web.Mvc;

    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;

    public class LogoutController : BaseController
    {
        #region Constructor

        public LogoutController(ILoggingService logger)
            : base(logger)
        {
        }

        #endregion Constructor

        #region Action methods

        /// <summary>
        /// Method to sign out the user and redirect them to the home page
        /// </summary>
        /// <returns>Redirect to home page</returns>
        [HttpPost]
        public ActionResult Logout()
        {
            if (IsLoggedIn)
            {
                Members.Logout();
            }

            return Redirect("/");
        }

        #endregion Action methods
    }
}
