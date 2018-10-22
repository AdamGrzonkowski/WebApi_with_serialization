using Helper.Common.Configuration;
using Xunit;

namespace Test.Common.Configuration
{
    public class ConfigKeyTest
    {
        [Fact]
        public void ApiBaseAddressHasCorrectKeyTest()
        {
            Assert.Equal("ApiBaseAddress", ConfigKey.ApiBaseAddress);
        }

        [Fact]
        public void PostRequestsUriHasCorrectKeyTest()
        {
            Assert.Equal("PostRequestsUri", ConfigKey.PostRequestsUri);
        }
    }
}
