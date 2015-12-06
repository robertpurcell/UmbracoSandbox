namespace UmbracoSandbox.Web.Controllers.Pages
{
    using System.Web.Mvc;

    using UmbracoSandbox.Service.Email;
    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Pages;
    using UmbracoSandbox.Web.Models.Forms;

    public class ContactController : BaseController
    {
        #region Fields

        private readonly IContactPageHandler _handler;
        private readonly IEmailService _mailer;

        #endregion Fields

        #region Constructor

        public ContactController(ILoggingService logger, IContactPageHandler handler, IEmailService mailer)
            : base(logger)
        {
            _handler = handler;
            _mailer = mailer;
        }

        #endregion Constructor

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Contact()
        {
            var vm = _handler.GetContactPageModel(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Method to send an email to the user
        /// </summary>
        /// <param name="vm">Contact page view model</param>
        /// <returns>Success message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxSendEmail(ContactFormViewModel vm)
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

            return Content(string.Format("<div class=\"alert alert-success\" role=\"alert\">{0}</div>", vm.ThankYouText), "text/html");
        }

        #endregion Action methods
    }
}
