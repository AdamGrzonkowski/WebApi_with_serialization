using Api;
using Api.Controllers;
using Application.Model;
using Application.Services.Interfaces;
using Helper.Common.Messages;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using Test.Builders;
using Xunit;

namespace Test.Web.Controllers
{
    public class RequestsControllerTest
    {
        private const int _numberOfRequests = 2;

        private readonly IEnumerable<RequestModel> _requests = RequestsGenerator.GetNRequestModels(_numberOfRequests);
        private readonly IRequestsService _service = NSubstitute.Substitute.For<IRequestsService>();

        [Fact]
        public async Task SaveToDatabaseReturnsCorrectNumberOfAddedItemsOnSuccess()
        {
            // Arrange
            _service.SaveRequestsToDbAsync(_requests).Returns(_numberOfRequests);
            RequestsController controller = new RequestsController(_service);

            // Act
            IHttpActionResult actionResult = await controller.SaveToDatabase(_requests);
            var contentResult = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(contentResult.Content, SuccessMessage.PostSuccess(_numberOfRequests));
        }

        [Fact]
        public async Task SaveToDatabaseSendsCorrectMessageOnSuccess()
        {
            // Arrange
            RequestsController controller = new RequestsController(_service);

            // Act
            IHttpActionResult actionResult = await controller.SaveDbRecordsToFile();
            var contentResult = actionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Equal(contentResult.Content, SuccessMessage.SavedToXml);
        }

        [Theory]
        [InlineData("http://localhost:54028/api/data", nameof(HttpMethod.Post), typeof(RequestsController), nameof(RequestsController.SaveToDatabase))]
        [InlineData("http://localhost:54028/api/jobs/saveFiles", nameof(HttpMethod.Get), typeof(RequestsController), nameof(RequestsController.SaveDbRecordsToFile))]
        public void EnsureCorrectControllerAndActionSelectedForUrl(string url, string method,
            Type controllerType, string actionName)
        {
            //Arrange
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            var controllerSelector = config.Services.GetHttpControllerSelector();
            var actionSelector = config.Services.GetActionSelector();

            var request = new HttpRequestMessage(new HttpMethod(method), url);

            config.EnsureInitialized();

            var routeData = config.Routes.GetRouteData(request);
            request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            //Act
            var ctrlDescriptor = controllerSelector.SelectController(request);
            var ctrlContext = new HttpControllerContext(config, routeData, request)
            {
                ControllerDescriptor = ctrlDescriptor
            };
            var actionDescriptor = actionSelector.SelectAction(ctrlContext);

            //Assert
            Assert.NotNull(ctrlDescriptor);
            Assert.Equal(controllerType, ctrlDescriptor.ControllerType);
            Assert.Equal(actionName, actionDescriptor.ActionName);
        }
    }
}
