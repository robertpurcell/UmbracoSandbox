namespace UmbracoSandbox.Web.Models
{
    using Zone.UmbracoMapper;

    public abstract class BasePageViewModel : BaseNodeViewModel
    {
        private string _metaTitle;

        public BasePageViewModel()
        {
        }

        #region Properties

        public string MetaTitle
        {
            get
            {
                return string.IsNullOrEmpty(_metaTitle) ? Name : _metaTitle;
            }

            set
            {
                _metaTitle = value;
            }
        }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string CanonicalUrl { get; set; }

        public string FacebookUrl { get; set; }

        public string LinkedInText { get; set; }

        public string LinkedInUrl { get; set; }

        public string TwitterText { get; set; }

        public string TwitterUrl { get; set; }

        public string OGSiteName { get; set; }

        public string OGTitle { get; set; }

        public string OGDescription { get; set; }

        public ImageModel OGImage { get; set; }

        public string OGImageUrl
        {
            get
            {
                return OGImage != null ? OGImage.Url : string.Empty;
            }
        }

        #endregion
    }
}
