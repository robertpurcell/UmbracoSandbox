namespace UmbracoSandbox.Web.Controllers.Content
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Content;
    using UmbracoSandbox.Web.Models.Forms;

    public class RegistrationController : BaseController
    {
        #region Fields

        private readonly IPageHandler _handler;

        #endregion

        #region Constructor

        public RegistrationController(IPageHandler handler)
        {
            _handler = handler;
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Registration()
        {
            var vm = _handler.GetPageModel<RegistrationViewModel>(CurrentPage);
            vm.Form.Days = DateTimeFormatInfo.InvariantInfo.DayNames.Select((name, i) => new SelectListItem
            {
                Value = i.ToString(),
                Text = name
            }).ToArray();
            vm.Form.Months = DateTimeFormatInfo.InvariantInfo.MonthNames.Where(x => !string.IsNullOrEmpty(x)).Select((name, i) => new SelectListItem
            {
                Value = i.ToString(),
                Text = name
            }).ToArray();
            vm.Form.Years = Enumerable.Range(DateTime.Now.Year, 10).Select((name, i) => new SelectListItem
            {
                Value = name.ToString(),
                Text = name.ToString()
            }).ToArray();

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Method to send an email to the user
        /// </summary>
        /// <param name="vm">Contact page view model</param>
        /// <returns>Success message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationForm vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToCurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }

        #endregion
    }
}
