namespace UmbracoSandbox.Web.Controllers.Pages
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Security;

    using UmbracoSandbox.Service.Email;
    using UmbracoSandbox.Service.Logging;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Pages;
    using UmbracoSandbox.Web.Models.Forms;

    public class RegistrationController : BaseController
    {
        #region Fields

        private readonly IRegistrationPageHandler _handler;
        private readonly IEmailService _mailer;

        #endregion Fields

        #region Constructor

        public RegistrationController(ILoggingService logger, IRegistrationPageHandler handler, IEmailService mailer)
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
        public ActionResult Registration()
        {
            var vm = _handler.GetRegistrationPageModel(CurrentPage, CurrentMember);

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Method to register the user
        /// </summary>
        /// <param name="vm">Registration form view model</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var member = Members.CreateRegistrationModel();
            member.Name = vm.Name;
            member.Email = vm.Email;
            member.Password = vm.Password;
            member.UsernameIsEmail = true;
            MembershipCreateStatus status;
            Members.RegisterMember(member, out status);
            if (!status.Equals(MembershipCreateStatus.Success))
            {
                return CurrentUmbracoPage();
            }

            var email = new EmailDetail
            {
                To = new List<string> { member.Email },
                Subject = "Registraion confirmation",
                IsBodyHtml = false
            };
            _mailer.Send(email);

            return RedirectToCurrentUmbracoPage();
        }

        #endregion Action methods
    }
}
