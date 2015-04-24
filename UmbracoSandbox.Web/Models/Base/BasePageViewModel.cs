namespace UmbracoSandbox.Web.Models
{
    using Umbraco.Core.Models;
    using UmbracoSandbox.Web.Models.Interfaces;
    using Zone.UmbracoMapper;

    public abstract class BasePageViewModel : BaseNodeViewModel, IMetadata, ITitle
    {
        #region Fields

        private string _title;
        private string _metaTitle;
        private string _metaTitleSuffix;
        private string _socialTitle;
        private string _socialDescription;

        #endregion

        #region Constructor

        public BasePageViewModel()
        {
        }

        #endregion

        #region Properties

        #region Metadata

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

        [PropertyMapping(MapRecursively = true)]
        public string MetaTitleSuffix
        {
            get
            {
                return string.IsNullOrEmpty(_metaTitleSuffix)
                    ? null
                    : string.Format(" | {0}", _metaTitleSuffix);
            }

            set
            {
                _metaTitleSuffix = value;
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

        #endregion

        public string Title
        {
            get
            {
                return string.IsNullOrEmpty(_title)
                    ? Name
                    : _title;
            }

            set
            {
                _title = value;
            }
        }

        #endregion

        public IPublishedContent CurrentPage { get; set; }
    }
}
