namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using UmbracoSandbox.Web.Models.Modules;

    public class ListingPageViewModel : BasePageViewModel
    {
        public IEnumerable<BlogPostModuleModel> Children { get; set; }
    }
}
