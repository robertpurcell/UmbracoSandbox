namespace UmbracoSandbox.Web.Handlers.Content
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Models.Content;
    using Zone.UmbracoMapper;

    public class RegistrationPageHandler : PageHandler, IRegistrationPageHandler
    {
        #region Constructor

        public RegistrationPageHandler(IUmbracoMapper mapper, INavigationHandler navigationHandler)
            : base(mapper, navigationHandler)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the model for the registration page
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="isLoggedIn">Whether or not the user is logged in</param>
        /// <returns>The page model</returns>
        public RegistrationViewModel GetRegistrationPageModel(IPublishedContent currentPage, bool isLoggedIn)
        {
            var model = GetPageModel<RegistrationViewModel>(currentPage, isLoggedIn);
            model.Form.Days = DateTimeFormatInfo.InvariantInfo.DayNames.Select((name, i) => new SelectListItem
            {
                Value = i.ToString(),
                Text = name
            }).ToArray();
            model.Form.Months = DateTimeFormatInfo.InvariantInfo.MonthNames.Where(x => !string.IsNullOrEmpty(x)).Select((name, i) => new SelectListItem
            {
                Value = i.ToString(),
                Text = name
            }).ToArray();
            model.Form.Years = Enumerable.Range(DateTime.Now.Year, 10).Select((name, i) => new SelectListItem
            {
                Value = name.ToString(),
                Text = name.ToString()
            }).ToArray();

            return model;
        }

        #endregion
    }
}
