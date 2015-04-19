﻿namespace UmbracoSandbox.Web.Controllers
{
    using System.Web.Mvc;
    using UmbracoSandbox.Service.EmailService;
    using UmbracoSandbox.Web.Models;
    using UmbracoSandbox.Web.Models.Modules;
    using Zone.UmbracoMapper;

    public class ListingPageController : BaseController
    {
        #region Constructor

        public ListingPageController(IUmbracoMapper mapper, IEmailService mailer)
            : base(mapper, mailer)
        {
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Populates the page view model and returns to the appropriate template
        /// </summary>
        /// <returns>ViewResult containing populated view model</returns>
        public ActionResult ListingPage()
        {
            var vm = GetPageModel<ListingPageViewModel>();
            Mapper.MapCollection<BlogPostModuleModel>(CurrentPage.Children, vm.Items);

            return CurrentTemplate(vm);
        }

        #endregion
    }
}
