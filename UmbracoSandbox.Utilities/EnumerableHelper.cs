namespace UmbracoSandbox.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableHelper
    {
        #region Enumerable helpers

        /// <summary>
        /// Checks whether or not the enumerable exists and contains any items
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="source">The enumerable</param>
        /// <returns>True or false</returns>
        public static bool IsAndAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        #endregion Enumerable helpers
    }
}
