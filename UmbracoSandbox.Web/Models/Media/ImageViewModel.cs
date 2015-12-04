namespace UmbracoSandbox.Web.Models.Media
{
    using System.Collections.Generic;

    using UmbracoSandbox.Web.Infrastructure.Config;

    using Zone.UmbracoMapper;

    public class ImageViewModel : FileViewModel
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

        [PropertyMapping(SourceProperty = PropertyAliases.UmbracoWidth)]
        public int Width { get; set; }

        [PropertyMapping(SourceProperty = PropertyAliases.UmbracoHeight)]
        public int Height { get; set; }

        public IDictionary<string, string> Crops { get; set; }

        #endregion
    }
}
