namespace UmbracoSandbox.Web.Models.Content
{
    using System;
    using System.Web;
    using UmbracoSandbox.Web.Models.Base;
    using UmbracoSandbox.Web.Models.Interfaces;

    public class BlogPostViewModel : BasePageViewModel, IBlogTitle
    {
        public DateTime Date { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public IHtmlString Standfirst { get; set; }
    }
}
