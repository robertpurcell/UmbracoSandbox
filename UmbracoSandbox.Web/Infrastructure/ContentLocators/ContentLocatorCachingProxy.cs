namespace UmbracoSandbox.Web.Infrastructure.ContentLocators
{
    using System;

    using Umbraco.Core.Models;
    using Umbraco.Web;

    using UmbracoSandbox.Web.Infrastructure.Config;

    public class ContentLocatorCachingProxy : IContentLocator
    {
        #region Fields

        private readonly IContentLocator _contentLocator;

        #endregion Fields

        #region Constructors

        public ContentLocatorCachingProxy(IContentLocator contentLocator)
        {
            _contentLocator = contentLocator;
        }

        #endregion Constructors

        #region Methods

        public IPublishedContent Find(IPublishedContent parent, string documentTypeAlias)
        {
            return FindFromCache(BuildCacheKey(documentTypeAlias), () => _contentLocator.Find(parent, documentTypeAlias));
        }

        public IPublishedContent Find(int id)
        {
            return FindFromCache(BuildCacheKey(id), () => _contentLocator.Find(id));
        }

        private static string BuildCacheKey(int id)
        {
            return BuildCacheKey(CacheKeys.Id, id.ToString());
        }

        private static string BuildCacheKey(string documentTypeAlias)
        {
            return BuildCacheKey(CacheKeys.DocumentTypeAlias, documentTypeAlias);
        }

        private static string BuildCacheKey(string keyType, string cacheKeySuffix)
        {
            return string.Format("{0}-{1}", keyType, cacheKeySuffix);
        }

        private static IPublishedContent FindFromCache(string cacheKey, Func<object> find)
        {
            // TODO need to encapsulate umbraco cache. Make static cache keys class
            // MUST BE DONE PER REQUEST DUE TO THREADING ISSUES
            // Cache for the current request only.
            // Content nodes cannot be cached in the application scope as it causes some issues when the cache is dropped (Suspected threading issues)
            return UmbracoContext.Current
                .Application
                .ApplicationCache
                .RequestCache
                .GetCacheItem(cacheKey, find) as IPublishedContent;
        }

        #endregion Methods
    }
}