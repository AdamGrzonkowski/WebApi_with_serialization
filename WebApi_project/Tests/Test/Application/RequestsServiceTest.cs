using Application.Services;
using Application.Services.Interfaces;
using Domain.Model;
using Domain.Services.Interfaces;
using Domain.Services.Interfaces.Base;
using Helper.Common.ConfigStrings;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Test.Builders;
using Xunit;

namespace Test.Application
{
    public class RequestsServiceTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IRequestRepository _repository;
        private readonly IRequestsService _requestsService;

        public RequestsServiceTest()
        {
            _uow = Substitute.For<IUnitOfWork>();
            _repository = Substitute.For<IRequestRepository>();

            _requestsService = new RequestsService(_uow, _repository);
        }

        [Fact]
        public async Task SaveRequestsToServiceTest()
        {
            const int number = 3;
            _uow.CommitAsync().Returns(number);

            int result = await _requestsService.SaveRequestsToDbAsync(RequestsGenerator.GetNRequests(number));
            Assert.Equal(number, result);
        }

        [Fact]
        public async Task WriteRequestsToFilesAsyncTest()
        {
            // Arrange
            const string reqName = "TestReq";
            DateTime dt = DateTime.Today;

            Request req1 = new Request
            {
                Name = reqName,
                Date = dt,
                Visits = 3
            };

            Request req2 = new Request
            {
                Name = "Some",
                Date = dt.AddDays(1)
            };

            string req1Date = req1.Date.ToString(Formatter.ShortDateFormat);
            string req2Date = req2.Date.ToString(Formatter.ShortDateFormat);

            var pathToSaveXmlFiles = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xmlTests");
            string filePath1 = Path.Combine(pathToSaveXmlFiles, req1Date + FileExtension.Xml);
            string filePath2 = Path.Combine(pathToSaveXmlFiles, req2Date + FileExtension.Xml);

            _repository.GetAllAsync().Returns(new List<Request> { req1, req2 });

            // Act
            await _requestsService.WriteRequestsToFilesAsync(pathToSaveXmlFiles);

            // Assert
            CheckXmlFileFormatting(filePath1, req1, req1Date);
            CheckXmlFileFormatting(filePath2, req2, req2Date);
        }

        private void CheckXmlFileFormatting(string filePath, Request req, string reqDateAsString)
        {
            Assert.True(File.Exists(filePath));

            XDocument doc = XDocument.Load(filePath);
            Assert.Equal("requests", doc.Root.Name);

            var reqElem = doc.Root.Element("request");
            Assert.NotNull(reqElem);

            var ixElem = reqElem.Element("ix");
            Assert.NotNull(ixElem);

            var contentElem = reqElem.Element("content");
            Assert.NotNull(contentElem);

            var nameElem = contentElem.Element("name");
            Assert.NotNull(nameElem);
            Assert.Equal(req.Name, nameElem.Value);

            var dateElem = contentElem.Element("dateRequested");
            Assert.NotNull(dateElem);
            Assert.Equal(reqDateAsString, dateElem.Value);

            var visElem = contentElem.Element("visits");
            if (req.Visits.HasValue)
            {
                Assert.NotNull(visElem);
                Assert.Equal(req.Visits.Value.ToString(), visElem.Value);
            }
            else
            {
                Assert.Null(visElem);
            }
        }
    }
}
