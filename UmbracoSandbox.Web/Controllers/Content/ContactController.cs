namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using UmbracoSandbox.Service.Email;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Content;
    using UmbracoSandbox.Web.Models.Forms;

    public class ContactController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;
        private readonly IEmailService _mailer;

        #endregion

        #region Constructor

        public ContactController(IPageHandler handler, IEmailService mailer)
        {
            _handler = handler;
            _mailer = mailer;
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Contact()
        {
            var vm = _handler.GetPageModel<ContactViewModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Method to send an email to the user
        /// </summary>
        /// <param name="vm">Contact page view model</param>
        /// <returns>Success message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxSendEmail(ContactForm vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ContactForm", vm);
            }

            var email = new EmailDetail
            {
                From = vm.Email,
                DisplayName = vm.FullName,
                Subject = vm.Subject,
                Body = vm.Message,
                IsBodyHtml = false
            };
            _mailer.Send(email);

            return Content("<div class=\"alert alert-success\" role=\"alert\"><strong>Thanks!</strong> I'll aim to reply to your message within 24 hours.</div>", "text/html");
        }

        #endregion
    }
}
