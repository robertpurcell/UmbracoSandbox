namespace UmbracoSandbox.Web.Models.Modules
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UmbracoSandbox.Web.Infrastructure.Config;

    public class BlogPostModuleViewModel : ModuleViewModel
    {
        [DisplayFormat(DataFormatString = DisplayFormats.ShortDate)]
        public DateTime Date { get; set; }

        public string Url { get; set; }
    }
}
