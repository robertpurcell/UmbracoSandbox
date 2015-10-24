namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System.Linq;

    using RJP.MultiUrlPicker.Models;

    using Umbraco.Core.Models;
    using Umbraco.Web;

    using Zone.UmbracoMapper;

    public class LinkMapper
    {
        #region Link mappers

        public static object GetLink(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var links = contentToMapFrom.GetPropertyValue<MultiUrls>(propertyAlias, recursive);

            return links != null ? links.SingleOrDefault() : null;
        }

        public static object GetLinks(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            return contentToMapFrom.GetPropertyValue<MultiUrls>(propertyAlias, recursive);
        }

        public static object GetLinkFromValue(IUmbracoMapper mapper, object value)
        {
            return value == null ? null : new MultiUrls(value.ToString()).FirstOrDefault();
        }

        public static object GetLinksFromValue(IUmbracoMapper mapper, object value)
        {
            return value == null ? null : new MultiUrls(value.ToString());
        }

        #endregion
    }
}
