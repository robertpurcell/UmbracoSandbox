namespace UmbracoSandbox.Web.Helpers
{
    using System;

    public static class DateHelper
    {
        /// <summary>
        /// Formats the DateTime object and returns a string
        /// </summary>
        /// <param name="d">Date time</param>
        /// <returns>Formatted string</returns>
        public static string FormatDate(DateTime d)
        {
            return d != DateTime.MinValue
                ? d.ToString("d MMMM yyyy")
                : string.Empty;
        }
    }
}
