namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Controllers.Base;

    public class LogoutController : BaseController
    {
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
    }
}
