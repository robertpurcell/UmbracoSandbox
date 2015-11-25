namespace UmbracoSandbox.Web.Models.Interfaces
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Umbraco.Core.Models.Membership;

    using UmbracoSandbox.Web.Infrastructure.Config;

    public interface IBlogTitle : ITitle
    {
        [DisplayFormat(DataFormatString = DisplayFormats.ShortDate)]
        DateTime Date { get; }

        IUser Author { get; }

        string ImageUrl { get; }

        IHtmlString Standfirst { get; }
    }
}
