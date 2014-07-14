namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using UmbracoSandbox.Web.Models;
    using Zone.UmbracoMapper;

    public class GoogleMapMapper
    {
        public static object GetGoogleMap(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            var temp = contentToMapFrom.GetPropertyValue<GoogleMap>(propertyAlias, recursive);
            return contentToMapFrom.GetPropertyValue<GoogleMap>(propertyAlias, recursive);
        }
    }
}
