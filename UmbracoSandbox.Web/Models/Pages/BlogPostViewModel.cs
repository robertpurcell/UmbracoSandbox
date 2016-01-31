namespace UmbracoSandbox.Web.Models.Pages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Umbraco.Core.Models.Membership;

    using UmbracoSandbox.Web.Infrastructure.Config;
    using UmbracoSandbox.Web.Models.Interfaces;

    public class BlogPostViewModel : BasePageViewModel, IBlogTitle
    {
        [DisplayFormat(DataFormatString = DisplayFormats.ShortDate)]
        public DateTime Date { get; set; }

        public IUser Author { get; set; }

        public string ImageUrl { get; set; }

        public IHtmlString Standfirst { get; set; }
    }
}
