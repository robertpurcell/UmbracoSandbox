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
        /// Applies dynamic background image styling
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="imageUrl">Image URL</param>
        /// <returns>Style attribute</returns>
        public static IHtmlString BackgroundImage(this HtmlHelper helper, string imageUrl)
        {
            return !string.IsNullOrEmpty(imageUrl)
                ? new HtmlString(string.Format("style=\"background-image:url({0}); background-repeat: no-repeat;\"", imageUrl))
                : null;
        }

        #endregion
    }
}
