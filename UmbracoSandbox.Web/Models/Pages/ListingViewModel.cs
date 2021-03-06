﻿namespace UmbracoSandbox.Web.Models.Pages
{
    using System.Collections.Generic;

    using UmbracoSandbox.Web.Models.Modules;

    public class ListingViewModel : BasePageViewModel
    {
        #region Constructor

        public ListingViewModel()
        {
            Items = new List<BlogPostModuleViewModel>();
        }

        #endregion Constructor

        #region Properties

        public IList<BlogPostModuleViewModel> Items { get; set; }

        #endregion Properties
    }
}
