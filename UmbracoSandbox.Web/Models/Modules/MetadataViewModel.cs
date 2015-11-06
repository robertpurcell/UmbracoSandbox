namespace UmbracoSandbox.Web.Models.Modules
{
    using UmbracoSandbox.Web.Models.Media;

    using Zone.UmbracoMapper;

    public class MetadataViewModel
    {
        #region Fields

        private string _metaTitle;
        private string _socialTitle;
        private string _socialDescription;

        #endregion Fields

        #region Properties

        public string Name { get; set; }

        public string MetaTitle
        {
            get
            {
                return string.IsNullOrEmpty(_metaTitle)
                    ? Name
                    : _metaTitle;
            }

            set
            {
                _metaTitle = value;
            }
        }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string CanonicalUrl { get; set; }

        public string AbsoluteUrl { get; set; }

        public string SocialUrl
        {
            get
            {
                return CanonicalUrl ?? AbsoluteUrl;
            }
        }

        [PropertyMapping(MapRecursively = true)]
        public string SocialSiteName { get; set; }

        public string SocialTitle
        {
            get
            {
                return string.IsNullOrEmpty(_socialTitle)
                    ? MetaTitle
                    : _socialTitle;
            }

            set
            {
                _socialTitle = value;
            }
        }

        public string SocialDescription
        {
            get
            {
                return string.IsNullOrEmpty(_socialDescription)
                    ? MetaDescription
                    : _socialDescription;
            }

            set
            {
                _socialDescription = value;
            }
        }

        public ImageModel SocialImage { get; set; }

        public string SocialImageUrl
        {
            get
            {
                return SocialImage != null
                    ? SocialImage.Url
                    : string.Empty;
            }
        }

        #endregion Properties
    }
}
