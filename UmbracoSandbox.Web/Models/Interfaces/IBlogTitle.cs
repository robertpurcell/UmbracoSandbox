namespace UmbracoSandbox.Web.Models.Interfaces
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using UmbracoSandbox.Web.Infrastructure.Config;

    public interface IBlogTitle : ITitle
    {
        [DisplayFormat(DataFormatString = DisplayFormats.ShortDate)]
        DateTime Date { get; }

        string Author { get; }

        string ImageUrl { get; }

        IHtmlString Standfirst { get; }
    }
}
