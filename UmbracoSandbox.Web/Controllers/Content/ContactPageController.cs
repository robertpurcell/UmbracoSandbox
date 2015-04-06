namespace UmbracoSandbox.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using UmbracoSandbox.Service.EmailService;
    using UmbracoSandbox.Web.Helpers;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class ContactPageController : BaseController
    {
        #region Fields

        private readonly IEmailService _emailService;

        #endregion

        #region Constructor

        public ContactPageController(IUmbracoMapper mapper, IEmailService emailService)
            : base(mapper)
        {
            _emailService = emailService;
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
        public ActionResult SendEmail(ContactPageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var email = new EmailDetail
                {
                    To = new List<string>() { ConfigHelper.GetSettingAsString("app.emailAddress") },
                    From = vm.Email,
                    DisplayName = vm.FullName,
                    Body = vm.Message,
                    IsBodyHtml = false
                };
                _emailService.Send(email);

                return RedirectToCurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }

        #endregion
    }
}
