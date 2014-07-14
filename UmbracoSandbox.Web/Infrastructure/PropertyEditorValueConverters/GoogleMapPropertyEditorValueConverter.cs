namespace UmbracoSandbox.Web.Infrastructure.PropertyEditorValueConverters
{
    using System;
    using Umbraco.Core;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Web;
 
    public class GoogleMapPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            return Guid.Parse("4023e540-92f5-11dd-ad8b-0800200c9a66").Equals(propertyEditorId);
        }
 
        public Attempt<object> ConvertPropertyValue(object value)
        {
            if (UmbracoContext.Current != null &&
                value != null &&
                value.ToString() != string.Empty)
            {
                return new Attempt<object>(true, value.ToString().Split(','));
            }
             
            return Attempt<object>.False;
        }
    }
}