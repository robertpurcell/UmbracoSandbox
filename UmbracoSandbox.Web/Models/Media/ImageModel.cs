namespace UmbracoSandbox.Web.Models
{
    using System.Collections.Generic;
    using Zone.UmbracoMapper;

    public class ImageModel : FileModel
    {
        #region Fields

        private string _altText;

        #endregion

        #region Properties

        public string AltText
        {
            get
            {
                return string.IsNullOrEmpty(_altText)
                    ? Name
                    : _altText;
            }

            set
            {
                _altText = value;
            }
        }

        [PropertyMapping(SourceProperty = "umbracoWidth")]
        public int Width { get; set; }

        [PropertyMapping(SourceProperty = "umbracoHeight")]
        public int Height { get; set; }

        public IDictionary<string, string> Crops { get; set; }

        #endregion
    }
}
