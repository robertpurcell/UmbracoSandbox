namespace UmbracoSandbox.Service.HttpClient
{
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface defining methods for HTTP web requests
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// Sent a GET request to the specified URI as an asynchronous operation
        /// </summary>
        /// <param name="url">Url to request</param>
        /// <returns>Task of HttpResponseMessage</returns>
        Task<HttpResponseMessage> GetAsync(string url);

        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);

        Task<HttpResponseMessage> GetAsJsonAsync(string url);

        Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T content);

        Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T content);

        void SetAuthenticationHeader(string username, string password);
    }
}
