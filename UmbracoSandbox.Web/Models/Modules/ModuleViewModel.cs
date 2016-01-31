namespace UmbracoSandbox.Web.Models.Modules
{
    using RJP.MultiUrlPicker.Models;

    using UmbracoSandbox.Web.Models.Media;

    public class ModuleViewModel : BaseModuleViewModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public ImageViewModel Image { get; set; }

        public Link Link { get; set; }
    }
}
