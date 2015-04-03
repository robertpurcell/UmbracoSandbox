namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using System.Web;
    using UmbracoSandbox.Web.Models.Interfaces;

    public class HomePageViewModel : BasePageViewModel, IHeroTitle
    {
        public ImageModel HeroImage { get; set; }

        public IHtmlString BodyText { get; set; }

        public IEnumerable<ModuleModel> Modules { get; set; }
    }
}
