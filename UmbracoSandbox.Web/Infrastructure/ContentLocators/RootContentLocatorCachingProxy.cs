namespace UmbracoSandbox.Web.Infrastructure.ContentLocators
{
    using System;

    using UmbracoSandbox.Web.Infrastructure.Config;

    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RootContentLocatorCachingProxy : IRootContentLocator
    {
        #region Fields

        private readonly IRootContentLocator _rootContentLocator;
        private readonly IContentLocator _contentLocator;

        #endregion Fields

        #region Constructors

        public RootContentLocatorCachingProxy(IRootContentLocator rootContentLocator, IContentLocator contentLocator)
        {
            _rootContentLocator = rootContentLocator;
            _contentLocator = contentLocator;
        }

        #endregion Constructors

        #region Methods

        public IPublishedContent Find()
        {
            var id = FindRootNodeId();

            return _contentLocator.Find(id);
        }

        private int FindRootNodeId()
        {
            return (int)UmbracoContext.Current
                .Application
                .ApplicationCache
                .RuntimeCache
                .GetCacheItem(CacheKeys.RootNodeId, () => RootNodeId(), new TimeSpan(1, 0, 0));
        }

        private int RootNodeId()
        {
            var root = _rootContentLocator.Find();

            if (root == null)
            {
                throw new ArgumentException("Root Node could not be found");
            }

            return root.Id;
        }

        #endregion Methods
    }
}