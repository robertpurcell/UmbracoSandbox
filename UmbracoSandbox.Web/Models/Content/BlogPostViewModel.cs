namespace UmbracoSandbox.Web.Models.Content
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Interfaces;

    public class BlogPostViewModel : BasePageViewModel, IBlogTitle
    {
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        public DateTime Date { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public IHtmlString Standfirst { get; set; }
    }
}
