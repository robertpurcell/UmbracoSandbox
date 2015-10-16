namespace UmbracoSandbox.Web.Models.Content
{
    using System.Collections.Generic;

    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Modules;

    public class ListingViewModel : BasePageViewModel
    {
        #region Constructor

        public ListingViewModel()
        {
            Items = new List<BlogPostModuleModel>();
        }

        #endregion Constructor

        #region Properties

        public IList<BlogPostModuleModel> Items { get; set; }

        #endregion Properties
    }
}
