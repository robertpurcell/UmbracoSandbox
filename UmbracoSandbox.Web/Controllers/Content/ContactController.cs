namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Service.EmailService;
    using UmbracoSandbox.Web.Handlers;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class ContactController : BaseController
    {
        #region Constructor

        public ContactController(IUmbracoMapper mapper, IPageHandler handler, IEmailService mailer)
            : base(mapper, handler)
        {
            Mailer = mailer;
        }

        #endregion

        #region Properties

        protected IEmailService Mailer { get; private set; }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Contact()
        {
            var vm = Handler.GetPageModel<ContactViewModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Method to send an email to the user
        /// </summary>
        /// <param name="vm">Contact page view model</param>
        /// <returns>Success message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxSendEmail(ContactViewModel vm)
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
