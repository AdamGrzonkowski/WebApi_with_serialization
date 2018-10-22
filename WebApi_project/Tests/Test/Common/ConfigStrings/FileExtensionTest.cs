using Helper.Common.ConfigStrings;
using Xunit;

namespace Test.Common.ConfigStrings
{
    public class FileExtensionTest
    {
        [Fact]
        public void XmlFileExtensionHasCorrectValue()
        {
            Assert.Equal(".xml", FileExtension.Xml);
        }
    }
}
