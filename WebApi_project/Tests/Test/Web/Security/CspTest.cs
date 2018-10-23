using Api.Security;
using Xunit;

namespace Test.Web.Security
{
    public class CspTest
    {
        [Fact]
        public void CspRuleTest()
        {
            Assert.Equal("default-src 'self'; style-src 'self'; frame-src 'self'; script-src 'self'; img-src 'self'; font-src 'self'", Csp.GetCspString());
        }

        [Fact]
        public void StyleSheetsRuleTest()
        {
            Assert.Equal("style-src 'self'", Csp.GetCspStyleSheetRule());
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
            Assert.Equal("script-src 'self'", Csp.GetCspScriptRule());
        }

        [Fact]
        public void ImgRuleTest()
        {
            Assert.Equal("img-src 'self'", Csp.GetCspImagesRule());
        }
    }
}
