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

        private const string PageTemplate = @"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeBehind=""{0}.aspx.cs"" Inherits=""UmbracoSandbox.Web.UI.{0}"" %>{1}";
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
        /// Request the page and save the raw HTML to disk.
        /// </summary>
        /// <param name="url">Error page URL</param>
        /// <param name="filename">The name of the destination file</param>
        /// <returns>Pubishing result</returns>
        public bool PublishWebFormsPage(string url, string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                return Task.Factory.StartNew(() => PublishWebFormsPageTask(url, filename).Result,
                        TaskCreationOptions.LongRunning).Result;
            }

            return false;
        }

        #endregion Interface methods

        #region Helpers

        /// <summary>
        /// Read the HTML from the page at the given URL and save it to an HTML file.
        /// </summary>
        /// <param name="url">The URL of the page</param>
        /// <param name="filename">The name of the destination file</param>
        /// <returns>True if successful</returns>
        private async Task<bool> PublishWebFormsPageTask(string url, string filename)
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
                        var content = string.Format(PageTemplate, filename, Environment.NewLine);
                        content += reader.ReadToEnd();
                        var path = Path.Combine(HttpRuntime.AppDomainAppPath, string.Format("{0}.aspx", filename));
                        File.WriteAllText(path, content);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _loggingService.Log(string.Format("Error publishing page: {0}", ex.Message), LogLevel.Error);
                return false;
            }
        }

        #endregion Helpers
    }
}
