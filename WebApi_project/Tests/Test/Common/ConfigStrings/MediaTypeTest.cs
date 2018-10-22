using Helper.Common.ConfigStrings;
using Xunit;

namespace Test.Common.ConfigStrings
{
    public class MediaTypeTest
    {
        [Fact]
        public void JsonMediaTypeHasCorrectValue()
        {
            Assert.Equal("application/json", MediaType.Json);
        }
    }
}
