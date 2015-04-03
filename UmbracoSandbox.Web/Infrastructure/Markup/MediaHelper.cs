namespace UmbracoSandbox.Web.Infrastructure.Markup
{
    using System.Web;
    using System.Web.Mvc;
    using UmbracoSandbox.Web.Models;

    public static class MediaHelper
    {
        #region Images

        public static IHtmlString DisplayImage(this HtmlHelper helper, ImageModel image, string cropAlias)
        {
            if (image == null || image.Crops == null || cropAlias == null || !image.Crops.ContainsKey(cropAlias))
            {
                return null;
            }

            var imgTag = new TagBuilder("img");
            imgTag.Attributes.Add("alt", image.AltText);
            imgTag.Attributes.Add("src", image.Crops[cropAlias]);
            var pictureTag = new TagBuilder("picture")
            {
                InnerHtml = imgTag.ToString(TagRenderMode.SelfClosing)
            };

            return new HtmlString(pictureTag.ToString());
        }

        #endregion
    }
}