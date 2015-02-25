namespace Zone.GoogleMaps
{
    using System;
    using Newtonsoft.Json;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.PropertyEditors;

    public class GoogleMapPropertyValueConverter : PropertyValueConverterBase
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
                LogHelper.Error<GoogleMapPropertyValueConverter>("Error converting GoogleMap property value  " + ex.StackTrace, ex);
                return null;
            }
        }
    }
}
