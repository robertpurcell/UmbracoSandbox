﻿namespace UmbracoSandbox.Web.Infrastructure.Helpers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using HtmlAgilityPack;

    public static class HtmlExtensions
    {
        public static IHtmlString ValidateHtml(this HtmlHelper helper, string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(input);
                if (htmlDoc.ParseErrors.Count() == 0)
                {
                    return new MvcHtmlString(input);
                }
            }

            return null;
        }
    }
}
