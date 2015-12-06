namespace UmbracoSandbox.Web.Models.Pages
{
    using System.Collections.Generic;

    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Modules;

    public class TimelineViewModel : BasePageViewModel
    {
        public IEnumerable<ModuleViewModel> Grid { get; set; }
    }
}
