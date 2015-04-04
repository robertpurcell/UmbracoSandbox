namespace UmbracoSandbox.Web.Helpers
{
    using System;
    using System.Configuration;

    public static class ConfigHelper
    {
        #region Config helpers

        public static int GetSettingAsInteger(string settingName)
        {
            return GetSettingAsType<int>(settingName, obj => Convert.ToInt32(obj));
        }

        public static string GetSettingAsString(string settingName)
        {
            return GetSettingAsType<string>(settingName, obj => Convert.ToString(obj));
        }

        public static bool GetSettingAsBoolean(string settingName)
        {
            return GetSettingAsType<bool>(settingName, obj => Convert.ToBoolean(obj));
        }

        public static DateTime GetSettingAsDateTime(string settingName)
        {
            return GetSettingAsType<DateTime>(settingName, obj => DateTime.Parse(obj as string));
        }

        public static decimal GetSettingAsDecimal(string settingName)
        {
            return GetSettingAsType<decimal>(settingName, obj => Convert.ToDecimal(obj));
        }

        #endregion

        #region Helper methods

        private static T GetSettingAsType<T>(string settingName, Func<object, T> callerConverter)
        {
            object obj = ConfigurationManager.AppSettings[settingName];
            var value = default(T);
            if (obj != null)
            {
                try
                {
                    value = callerConverter(obj);
                }
                catch
                {
                    value = default(T);
                }
            }

            return value;
        }

        #endregion
    }
}
