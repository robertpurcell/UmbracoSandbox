namespace UmbracoSandbox.Web.Models.Modules
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using UmbracoSandbox.Web.Infrastructure.Config;
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Media;

    public class TimelineEntryViewModel : BaseModuleViewModel
    {
        public string Title { get; set; }

        public IHtmlString Summary { get; set; }

        public ImageViewModel Image { get; set; }

        [DisplayFormat(DataFormatString = DisplayFormats.ShortDate)]
        public DateTime Date { get; set; }
    }
}
