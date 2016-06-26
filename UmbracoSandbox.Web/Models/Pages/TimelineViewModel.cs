namespace UmbracoSandbox.Web.Models.Pages
{
    using System.Collections.Generic;

    using UmbracoSandbox.Common.Helpers;
    using UmbracoSandbox.Web.Models.Modules;

    public class TimelineViewModel : BasePageViewModel
    {
        public IEnumerable<TimelineEntryViewModel> Timeline { get; set; }

        public bool ShowTimeline => Timeline.IsAndAny();
    }
}
