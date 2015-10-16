namespace UmbracoSandbox.Web.Helpers
{
    using System;
    using System.Configuration;

    public static class ConfigHelper
    {
        #region Config helpers

        public static int GetSettingAsInteger(string settingName)
        {
            return GetSettingAsType(settingName, Convert.ToInt32);
        }

        public static string GetSettingAsString(string settingName)
        {
            return GetSettingAsType(settingName, Convert.ToString);
        }

        public static bool GetSettingAsBoolean(string settingName)
        {
            return GetSettingAsType(settingName, Convert.ToBoolean);
        }

        public static DateTime GetSettingAsDateTime(string settingName)
        {
            return GetSettingAsType(settingName, obj => DateTime.Parse(obj as string));
        }

        public static decimal GetSettingAsDecimal(string settingName)
        {
            return GetSettingAsType(settingName, Convert.ToDecimal);
        }

        #endregion Config helpers

        #region Helper methods

        private static T GetSettingAsType<T>(string settingName, Func<object, T> callerConverter)
        {
            var obj = ConfigurationManager.AppSettings[settingName];
            var value = default(T);
            if (obj == null)
            {
                return value;
            }

            try
            {
                value = callerConverter(obj);
            }
            catch
            {
                value = default(T);
            }

            return value;
        }

        #endregion Helper methods
    }
}
