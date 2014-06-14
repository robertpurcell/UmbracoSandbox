namespace UmbracoSandbox.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public static class MappingExtensions
    {
        /// <summary>
        /// Get method for string property values
        /// </summary>
        /// <param name="item">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <returns>Property value as string</returns>
        public static string Get(this IPublishedContent item, string get)
        {
            return item.GetProperty(get) != null && item.GetProperty(get).Value != null ? item.GetProperty(get).Value.ToString() : string.Empty;
        }

        /// <summary>
        /// Recursive Get method for string property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property name</param>
        /// <param name="recursive">Boolean stating whether to look for the property recursively</param>
        /// <returns>Property value as string</returns>
        public static string Get(this IPublishedContent content, string get, bool recursive)
        {
            return content.GetPropertyValue<string>(get, recursive, string.Empty);
        }

        /// <summary>
        /// Get method for integer property values
        /// </summary>
        /// <param name="content">IPublishedContent page</param>
        /// <param name="get">String of the property</param>
        /// <returns>Property value as integer or zero</returns>
        public static int GetIdValueOrZero(this IPublishedContent content, string get)
        {
            return content.GetProperty(get) == null || content.GetProperty(get).Value.ToString() == string.Empty
                        ? 0
                        : int.Parse(content.GetProperty(get).Value.ToString());
        }

        /// <summary>
        /// Gets a list of IPublishedContent from multi-node tree picker property
        /// </summary>
        /// <param name="item">IPublishedContent page</param>
        /// <param name="get">String of the property name</param>
        /// <returns>IEnumerable of IPublishedContent objects</returns>
        public static IEnumerable<IPublishedContent> GetMntpItemsByCsv(this IPublishedContent item, string get)
        {
            var csv = item.Get(get);
            IEnumerable<IPublishedContent> items;
            if (!string.IsNullOrEmpty(csv))
            {
                IEnumerable<int> mntpCsv = csv
                   .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(x => int.Parse(x));

                items = new UmbracoHelper(UmbracoContext.Current)
                    .TypedContent(mntpCsv)
                    .Where(x => x != null);
                return items;
            }

            return Enumerable.Empty<IPublishedContent>();
        }

        /// <summary>
        /// Gets a media property as IPublishedContent
        /// </summary>
        /// <param name="item">The page</param>
        /// <param name="get">String of the property name</param>
        /// <param name="recursive">Boolean stating whether to look for the property recursively</param>
        /// <returns>IPublishedContent or null</returns>
        public static IPublishedContent GetMedia(this IPublishedContent item, string get, bool recursive = false)
        {
            var media = new UmbracoHelper(UmbracoContext.Current).TypedMedia(item.Get(get, recursive));

            return media;
        }
    }
}
