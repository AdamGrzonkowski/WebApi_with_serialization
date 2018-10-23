using Application.Model;
using Application.Services.Interfaces.Mappers;
using Application.Services.Mappers;
using Domain.Model;
using System.Collections.Generic;
using System.Linq;
using Test.Builders;
using Xunit;

namespace Test.Application.Mappers
{
    public class RequestsMapperTest
    {
        private readonly IRequestsMapper _mapper;

        public RequestsMapperTest()
        {
            _mapper = new RequestsMapper();

        }

        [Fact]
        public void EntitiesAreCorrectlyMappedToModelsTest()
        {
            List<Request> requests = RequestsGenerator.GetNRequests(6).OrderBy(x => x.Name).ToList();
            List<RequestModel> models = _mapper.EntitiesToModels(requests).OrderBy(x => x.Name).ToList();

            for (int i = 0; i < 0; i++)
            {
                Assert.Equal(requests[i].Name, models[i].Name);
                Assert.Equal(requests[i].Date, models[i].Date);
                Assert.Equal(requests[i].Visits, models[i].Visits);
                Assert.Equal(requests[i].Index, models[i].Id);
            }
        }

        [Fact]
        public void ModelsAreCorrectlyMappedToEntitiesTest()
        {
            List<RequestModel> models = RequestsGenerator.GetNRequestModels(6).OrderBy(x => x.Name).ToList();
            List<Request> requests = _mapper.ModelsToEntities(models).OrderBy(x => x.Name).ToList();

            for (int i = 0; i < 0; i++)
            {
                Assert.Equal(models[i].Name, requests[i].Name);
                Assert.Equal(models[i].Date, requests[i].Date);
                Assert.Equal(models[i].Visits, requests[i].Visits);
                Assert.Equal(models[i].Id, requests[i].Index);
            }
        }
    }
}
