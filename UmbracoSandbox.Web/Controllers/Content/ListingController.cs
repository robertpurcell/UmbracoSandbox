﻿namespace UmbracoSandbox.Web.Controllers.Content
{
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Controllers.Base;
    using UmbracoSandbox.Web.Handlers.Content;
    using UmbracoSandbox.Web.Models.Modules;

    public class ListingController : BaseController
    {
        #region Fields

        private readonly IListingPageHandler _handler;

        #endregion

        #region Constructor

        public ListingController(IListingPageHandler handler)
        {
            _handler = handler;
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult Listing()
        {
            var vm = _handler.GetListingPageModel<BlogPostModuleModel>(CurrentPage);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
