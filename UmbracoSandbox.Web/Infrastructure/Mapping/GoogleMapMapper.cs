namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    using Zone.GoogleMaps;
    using Zone.UmbracoMapper;

    public class GoogleMapMapper
    {
        public static object GetGoogleMap(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyAlias, bool recursive)
        {
            return contentToMapFrom.GetPropertyValue<GoogleMap>(propertyAlias, recursive);
        }
    }
}
