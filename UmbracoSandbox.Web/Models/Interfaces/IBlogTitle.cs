namespace UmbracoSandbox.Web.Models.Interfaces
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public interface IBlogTitle : ITitle
    {
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        DateTime Date { get; }

        string Author { get; }

        string ImageUrl { get; }

        IHtmlString Standfirst { get; }
    }
}
