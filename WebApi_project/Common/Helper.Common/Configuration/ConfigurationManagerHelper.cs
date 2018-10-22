using System.Configuration;

namespace Helper.Common.Configuration
{
    public static class ConfigurationManagerHelper
    {
        /// <summary>
        /// Reads the configuration from appSettings.
        /// </summary>
        /// <param name="key"></param>
        public static string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
