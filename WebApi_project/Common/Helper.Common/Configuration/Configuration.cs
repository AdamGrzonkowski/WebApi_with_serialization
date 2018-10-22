namespace Helper.Common.Configuration
{
    public class Configuration : IConfiguration
    {
        /// <inheritdoc />
        public string BaseApiAddress => ConfigurationManagerHelper.ReadConfig(ConfigKey.ApiBaseAddress);
        /// <inheritdoc />
        public string PostRequestsUri => ConfigurationManagerHelper.ReadConfig(ConfigKey.PostRequestsUri);
    }
}
