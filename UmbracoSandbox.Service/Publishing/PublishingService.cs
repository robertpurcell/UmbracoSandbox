namespace UmbracoSandbox.Service.Publishing
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;

    public class PublishingService : IPublishingService
    {
        #region Fields

        private readonly string _pageNotFoundFileName;
        private readonly string _serverErrorFileName;
        private readonly string _errorPagePublishingHostName;

        #endregion

        #region Constructor

        public PublishingService(string pageNotFoundFileName, string serverErrorFileName, string errorPagePublishingHostName)
        {
            _pageNotFoundFileName = pageNotFoundFileName;
            _serverErrorFileName = serverErrorFileName;
            _errorPagePublishingHostName = errorPagePublishingHostName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Request the error page for the appropriate error code and save the raw html to disk.
        /// </summary>
        /// <param name="url">Error page URL</param>
        /// <param name="errorCode">Error code</param>
        /// <returns>Whether successful</returns>
        public bool PublishErrorPage(string url, int errorCode)
        {
            switch (errorCode)
            {
                case 404:
                    return Task.Factory.StartNew(() => PublishPage(url, _pageNotFoundFileName).Result,
                        TaskCreationOptions.LongRunning).Result;
                case 500:
                    return Task.Factory.StartNew(() => PublishPage(url, _serverErrorFileName).Result,
                        TaskCreationOptions.LongRunning).Result;
                default:
                    return false;
            }
        }

        #endregion

        #region Helpers

        private async Task<bool> PublishPage(string url, string filename)
        {
            var fullUrl = string.Format("http://{0}{1}", _errorPagePublishingHostName, url);
            var builder = new UriBuilder(fullUrl)
            {
                Query = "force-http-status=200"
            };
            var request = (HttpWebRequest)WebRequest.Create(builder.ToString());
            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    var stream = response.GetResponseStream();
                    if (stream == null)
                    {
                        return false;
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var content = reader.ReadToEnd();
                        var path = Path.Combine(HttpRuntime.AppDomainAppPath, filename);
                        File.WriteAllText(path, content);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
