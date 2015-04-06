namespace UmbracoSandbox.Web.Models.Interfaces
{
    using System;
    using System.Web;

    public interface IBlogTitle : ITitle
    {
        DateTime Date { get; }

        string Author { get; }

        string ImageUrl { get; }

        IHtmlString Standfirst { get; }
    }
}
