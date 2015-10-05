namespace Zone.GoogleMaps
{
    using System;
    using Newtonsoft.Json;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.PropertyEditors;

    public class GoogleMapPropertyValueConverter : PropertyValueConverterBase
    {
        /// <summary>
        /// Checks whether or not the property is of the correct type
        /// </summary>
        /// <param name="propertyType">Property type</param>
        /// <returns>True if correct</returns>
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("Zone.GoogleMaps");
        }

        /// <summary>
        /// Converts property source value to Google Map object
        /// </summary>
        /// <param name="propertyType">Property type</param>
        /// <param name="source">Source object</param>
        /// <param name="preview">Is preview</param>
        /// <returns>Converted object</returns>
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null || string.IsNullOrWhiteSpace(source.ToString()))
            {
                return null;
            }

            try
            {
                var obj = JsonConvert.DeserializeObject<GoogleMap>(source.ToString());

                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.Error<GoogleMapPropertyValueConverter>(string.Format("Error converting GoogleMap property value: {0}", ex.StackTrace), ex);

                return null;
            }
        }
    }
}
