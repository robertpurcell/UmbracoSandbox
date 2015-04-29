namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using UmbracoSandbox.Web.Models.Interfaces;

    public class HomeViewModel : BasePageViewModel, IHeroTitle
    {
        public ImageModel HeroImage { get; set; }

        public IEnumerable<ModuleModel> Modules { get; set; }
    }
}
