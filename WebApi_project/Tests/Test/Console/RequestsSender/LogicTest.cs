using Helper.Common.Configuration;
using Helper.Common.Http;
using Helper.Common.Messages;
using NSubstitute;
using RequestsSender.Core;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Test.Console.RequestsSender
{
    public class LogicTest
    {
        private readonly ILogic _logic;
        private readonly IConfiguration _config;
        private readonly IHttpHandler _httpHandler;

        private const int _argument = 3;

        public LogicTest()
        {
            _config = Substitute.For<IConfiguration>();
            _httpHandler = Substitute.For<IHttpHandler>();
            _logic = new Logic(_config, _httpHandler);
        }

        [Fact]
        public async Task SingleIntegerMustBePassedAsArgument()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _logic.RunAsync(new[] { "dsds" }));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _logic.RunAsync(new[] { "13", "3", "5" }));
        }

        [Fact]
        public async Task ThrowErrorIfStatusCodeFromApiWasNotSuccess()
        {
            ArrangeHttpRequest(HttpStatusCode.BadRequest);
            await Assert.ThrowsAsync<HttpRequestException>(async () => await _logic.RunAsync(new[] { $"{_argument}" }));
        }

        [Fact]
        public async Task NoErrorIfApiRespondsWithStatusSuccess()
        {
            ArrangeHttpRequest(HttpStatusCode.OK);
            await _logic.RunAsync(new[] { $"{_argument}" });
        }

        private void ArrangeHttpRequest(HttpStatusCode code)
        {
            HttpContent respContent = new StringContent(SuccessMessage.PostSuccess(_argument));

            _config.BaseApiAddress.Returns("http://localhost:12345");
            _config.PostRequestsUri.Returns("someAddress");

            _httpHandler.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>()).Returns(new HttpResponseMessage
            {
                StatusCode = code,
                Content = respContent
            });
        }
    }
}
