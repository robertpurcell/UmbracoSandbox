namespace UmbracoSandbox.Web.Handlers.Content
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using Umbraco.Core.Models;

    using UmbracoSandbox.Web.Handlers.Modules;
    using UmbracoSandbox.Web.Handlers.Navigation;
    using UmbracoSandbox.Web.Models.Content;

    using Zone.UmbracoMapper;

    public class RegistrationPageHandler : PageHandler, IRegistrationPageHandler
    {
        #region Constructor

        public RegistrationPageHandler(IUmbracoMapper mapper, IMetadataHandler metadataHandler, INavigationHandler navigationHandler)
            : base(mapper, metadataHandler, navigationHandler)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the model for the registration page
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="currentMember">The current member</param>
        /// <returns>The page model</returns>
        public RegistrationViewModel GetRegistrationPageModel(IPublishedContent currentPage, IMember currentMember)
        {
            var model = GetPageModel<RegistrationViewModel>(currentPage, currentMember);
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

        #endregion Methods
    }
}
