namespace UmbracoSandbox.Service.Helpers
{
    using System.Collections.Generic;

    public static class DatabaseResultHelper
    {
        /// <summary>
        /// Count the number of times each item occurs in a set
        /// </summary>
        /// <typeparam name="T">Type of the items to count</typeparam>
        /// <param name="items">Items to count</param>
        /// <returns>
        /// A dictionary where the key is an item from the input list, 
        /// and the value is the number of times that item occurs
        /// </returns>
        public static Dictionary<T, int> CountOccurances<T>(IEnumerable<T> items)
        {
            var result = new Dictionary<T, int>();
            foreach (var item in items)
            {
                if (result.ContainsKey(item))
                {
                    result[item]++;
                }
                else
                {
                    result.Add(item, 1);
                }
            }

            return result;
        }
    }
}
