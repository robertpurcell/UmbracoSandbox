namespace UmbracoSandbox.Service.Publishing
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;

    using UmbracoSandbox.Service.Logging;

    public class PublishingService : IPublishingService
    {
        #region Fields

        private readonly ILoggingService _loggingService;

        #endregion Fields

        #region Constructor

        public PublishingService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        #endregion Constructor

        #region Interface methods

        /// <summary>
        /// Request the error page for the appropriate error code and save the raw html to disk.
        /// </summary>
        /// <param name="url">Error page URL</param>
        /// <param name="errorCode">Error code</param>
        /// <returns>Whether successful</returns>
        public bool PublishErrorPage(string url, int errorCode)
        {
            if (errorCode != 0)
            {
                    return Task.Factory.StartNew(() => PublishPage(url, string.Format("{0}.html", errorCode)).Result,
                        TaskCreationOptions.LongRunning).Result;
            }

            return false;
        }

        #endregion Interface methods

        #region Helpers

        /// <summary>
        /// Read the HTML from the page at the given URL and save it to an HTML file
        /// </summary>
        /// <param name="url">The URL of the page</param>
        /// <param name="filename">The name of the destination file</param>
        /// <returns>True if successful</returns>
        private async Task<bool> PublishPage(string url, string filename)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                using (var response = await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                {
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
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error publishing static error page: {0}", ex.Message), LogLevel.Error);
                return false;
            }
        }

        #endregion Helpers
    }
}
