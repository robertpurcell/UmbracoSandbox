namespace UmbracoSandbox.Web.Models.Modules
{
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Media;

    public class ModuleModel : BaseModuleModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public ImageModel Image { get; set; }
    }
}
