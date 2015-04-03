namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using System.Web;

    public class HomePageViewModel : BasePageViewModel
    {
        public string Title { get; set; }

        public IHtmlString BodyText { get; set; }

        public ImageModel HeroImage { get; set; }

        public IEnumerable<ModuleModel> Modules { get; set; }
    }
}
