namespace Zone.Grid
{
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.PropertyEditors;

    using UmbracoSandbox.Common.Helpers;

    public class GridPropertyValueConverter : PropertyValueConverterBase
    {
        /// <summary>
        /// Checks whether or not the property is of the correct type
        /// </summary>
        /// <param name="propertyType">Property type</param>
        /// <returns>True if correct</returns>
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals("Umbraco.Grid");
        }

        /// <summary>
        /// Converts property source value to Grid object
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

            return JsonHelper.Deserialize<Grid>(source.ToString());
        }
    }
}
