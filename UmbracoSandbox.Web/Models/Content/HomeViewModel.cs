namespace UmbracoSandbox.Web.Models.Content
{
    using System.Collections.Generic;

    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Interfaces;
    using UmbracoSandbox.Web.Models.Media;
    using UmbracoSandbox.Web.Models.Modules;

    public class HomeViewModel : BasePageViewModel, IHeroTitle
    {
        public ImageModel HeroImage { get; set; }

        public IEnumerable<ModuleViewModel> Modules { get; set; }
    }
}
