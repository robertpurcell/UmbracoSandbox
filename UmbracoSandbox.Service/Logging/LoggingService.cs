namespace UmbracoSandbox.Service.Logging
{
    using log4net;
    using log4net.Config;

    /// <summary>
    /// Class implementing methods for logging data service calls to a file
    /// </summary>
    public class LoggingService : ILoggingService
    {
        #region Fields

        private readonly ILog _log;

        #endregion

        #region Constructor

        public LoggingService()
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Interface methods

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="level">The level</param>
        public void Log(string message, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _log.Debug(message);
                    break;
                case LogLevel.Info:
                    _log.Info(message);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
            }
        }

        #endregion
    }
}
