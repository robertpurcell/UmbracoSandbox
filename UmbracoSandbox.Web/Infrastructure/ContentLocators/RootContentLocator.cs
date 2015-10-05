namespace UmbracoSandbox.Web.Infrastructure.ContentLocators
{
    using System.Linq;

    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RootContentLocator : IRootContentLocator
    {
        #region Fields

        private readonly UmbracoHelper _umbracoHelper;

        #endregion Fields

        #region Constructors

        public RootContentLocator()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        #endregion Constructors

        #region Methods

        public IPublishedContent Find()
        {
            return _umbracoHelper.TypedContentAtRoot().First();
        }

        #endregion Methods
    }
}