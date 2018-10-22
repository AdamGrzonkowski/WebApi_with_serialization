namespace Helper.Common.Configuration
{
    /// <summary>
    /// Facade to wrap around configuration manager - allows easy mocking.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Base address of Api in the format
        /// protocol:ip/dns:port/
        /// </summary>
        string BaseApiAddress { get; }

        /// <summary>
        /// Uri of Post method on Requests controller.
        /// </summary>
        string PostRequestsUri { get; }
    }
}
