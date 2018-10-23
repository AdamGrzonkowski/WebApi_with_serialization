using Api.Security;
using Xunit;

namespace Test.Web.Security
{
    public class CspTest
    {
        [Fact]
        public void CspRuleTest()
        {
            Assert.Equal("default-src 'self'; style-src 'self' 'unsafe-inline'; frame-src 'self'; script-src 'self' 'sha256-NIDT1bUKf5Ez3feQSP65cgv5YGrWo7EEQjUGoP7TnLs='; img-src 'self'; font-src 'self'", Csp.GetCspString());
        }

        [Fact]
        public void StyleSheetsRuleTest()
        {
            Assert.Equal("style-src 'self' 'unsafe-inline'", Csp.GetCspStyleSheetRule());
        }

        [Fact]
        public void FontRuleTest()
        {
            Assert.Equal("font-src 'self'", Csp.GetCspFontsRule());
        }

        [Fact]
        public void FrameRuleTest()
        {
            Assert.Equal("frame-src 'self'", Csp.GetCspFrameRule());
        }

        [Fact]
        public void ScriptsRuleTest()
        {
            Assert.Equal("script-src 'self' 'sha256-NIDT1bUKf5Ez3feQSP65cgv5YGrWo7EEQjUGoP7TnLs='", Csp.GetCspScriptRule());
        }

        [Fact]
        public void ImgRuleTest()
        {
            Assert.Equal("img-src 'self'", Csp.GetCspImagesRule());
        }
    }
}
