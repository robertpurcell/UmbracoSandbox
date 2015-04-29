namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using UmbracoSandbox.Web.Models.Modules;

    public class ListingViewModel : BasePageViewModel
    {
        #region Constructor

        public ListingViewModel()
        {
            Items = new List<BlogPostModuleModel>();
        }

        #endregion

        #region Properties

        public IList<BlogPostModuleModel> Items { get; set; }

        #endregion
    }
}
