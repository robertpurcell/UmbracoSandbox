namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using UmbracoSandbox.Web.Models.Modules;

    public class ListingPageViewModel : BasePageViewModel
    {
        #region Constructor

        public ListingPageViewModel()
        {
            Items = new List<BlogPostModuleModel>();
        }

        #endregion

        public IList<BlogPostModuleModel> Items { get; set; }
    }
}
