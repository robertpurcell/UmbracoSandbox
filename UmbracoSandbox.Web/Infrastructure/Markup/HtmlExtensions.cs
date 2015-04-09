namespace UmbracoSandbox.Web.Infrastructure.Markup
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using HtmlAgilityPack;

    public static class HtmlExtensions
    {
        #region Html extensions

        /// <summary>
        /// Validates Html input
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="input">Html input</param>
        /// <returns>Validated output</returns>
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

        /// <summary>
        /// Outputs Html attribute
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Html attribute</returns>
        public static IHtmlString Attribute(this HtmlHelper helper, string name, string value)
        {
            return new MvcHtmlString(string.IsNullOrEmpty(value) ? null : string.Format(@" {0}=""{1}""", name, value));
        }

        #endregion
    }
}
