namespace UmbracoSandbox.Service.Helpers
{
    using Newtonsoft.Json;

    public static class JsonHelper
    {
        /// <summary>
        /// Helper method to deserialise the web service response
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="source">The source string</param>
        /// <returns>Object of the given type</returns>
        public static T Deserialize<T>(string source)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(source);

                return obj;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
