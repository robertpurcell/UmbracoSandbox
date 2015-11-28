namespace UmbracoSandbox.Service.HttpClient
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class wrapping HttpClient web requests
    /// </summary>
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _client;

        public HttpClientWrapper(int timeoutInSeconds)
        {
            _client = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, timeoutInSeconds)
            };

            // Ensure requests don't fail due to an invalid SSL certificate (found in certain test environments)
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Sent a GET request to the specified URI as an asynchronous operation
        /// </summary>
        /// <param name="url">Url to request</param>
        /// <returns>Task of HttpResponseMessage</returns>
        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _client.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return _client.PostAsync(url, content);
        }

        public Task<HttpResponseMessage> GetAsJsonAsync(string url)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _client.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T content)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _client.PostAsJsonAsync(url, content);
        }

        public Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T content)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return _client.PutAsJsonAsync(url, content);
        }

        public void SetAuthenticationHeader(string username, string password)
        {
            var credentials = string.Format("{0}:{1}", username, password);
            var bytes = Encoding.ASCII.GetBytes(credentials);
            var base64 = Convert.ToBase64String(bytes);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
        }
    }
}
