namespace UmbracoSandbox.Web.Infrastructure.ContentLocators
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class ContentLocator : IContentLocator
    {
        #region Fields

        private readonly UmbracoHelper _umbracoHelper;

        #endregion Fields

        #region Constructors

        public ContentLocator()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        #endregion Constructors

        #region Methods

        public IPublishedContent Find(IPublishedContent parent, string documentTypeAlias)
        {
            return parent.Descendant(documentTypeAlias);
        }

        public IPublishedContent Find(int id)
        {
            return _umbracoHelper.TypedContent(id);
        }

        #endregion Methods
    }
}