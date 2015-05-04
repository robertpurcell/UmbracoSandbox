namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using UmbracoSandbox.Web.Models.Interfaces;
    using UmbracoSandbox.Web.Models.Modules;

    public class HomeViewModel : BasePageViewModel, IHeroTitle
    {
        public ImageModel HeroImage { get; set; }

        public IEnumerable<ModuleModel> Modules { get; set; }
    }
}
