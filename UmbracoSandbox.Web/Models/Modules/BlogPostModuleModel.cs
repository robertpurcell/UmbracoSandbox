namespace UmbracoSandbox.Web.Models.Modules
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BlogPostModuleModel : ModuleModel
    {
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        public DateTime Date { get; set; }

        public string Url { get; set; }
    }
}
