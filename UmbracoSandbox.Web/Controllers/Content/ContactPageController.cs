namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Service.EmailService;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class ContactPageController : BaseController
    {
        #region Constructor

        public ContactPageController(IUmbracoMapper mapper, IEmailService mailer)
            : base(mapper, mailer)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult ContactPage()
        {
            var vm = GetPageModel<ContactPageViewModel>();

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Method to send an email to the user
        /// </summary>
        /// <param name="vm">Contact page view model</param>
        /// <returns>Success message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxSendEmail(ContactPageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var email = new EmailDetail
                {
                    From = vm.Email,
                    DisplayName = vm.FullName,
                    Subject = vm.Subject,
                    Body = vm.Message,
                    IsBodyHtml = false
                };
                Mailer.Send(email);

                return Content("<div class=\"alert alert-success\" role=\"alert\"><strong>Thanks!</strong> I'll aim to reply to your message within 24 hours.</div>", "text/html");
            }

            return CurrentUmbracoPage();
        }

        #endregion
    }
}
