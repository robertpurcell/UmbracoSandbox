namespace UmbracoSandbox.Web.Models.Base
{
    using System.Web;

    using UmbracoSandbox.Web.Models.Interfaces;
    using UmbracoSandbox.Web.Models.Modules;
    using UmbracoSandbox.Web.Models.Navigation;

    using Zone.UmbracoMapper;

    public abstract class BasePageViewModel : BaseNodeViewModel, IBodyText, ITitle
    {
        #region Fields

        private string _metaTitle;
        private string _metaTitleSuffix;
        private string _title;

        #endregion Fields

        #region Properties

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

        [PropertyMapping(MapRecursively = true)]
        public IHtmlString CookiePolicy { get; set; }

        public IHtmlString BodyText { get; set; }

        public MetadataViewModel Metadata { get; set; }

        public MainNavigationViewModel MainNavigation { get; set; }

        public NavigationViewModel FooterNavigation { get; set; }

        #endregion Properties
    }
}
