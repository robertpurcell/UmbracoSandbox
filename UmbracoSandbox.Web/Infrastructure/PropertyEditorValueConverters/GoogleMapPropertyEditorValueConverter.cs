namespace UmbracoSandbox.Web.Infrastructure.PropertyEditorValueConverters
{
    using System;
    using Newtonsoft.Json;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.PropertyEditors;
    using UmbracoSandbox.Web.Models;

    public class GoogleMapPropertyEditorValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return "Zone.GoogleMaps".Equals(propertyType.PropertyEditorAlias);
        }

        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null || string.IsNullOrWhiteSpace(source.ToString()))
            {
                return null;
            }

            var sourceString = source.ToString();
            try
            {
                var obj = JsonConvert.DeserializeObject<GoogleMap>(sourceString);

                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}