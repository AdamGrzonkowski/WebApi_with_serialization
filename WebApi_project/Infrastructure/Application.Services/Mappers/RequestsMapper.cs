using Application.Model;
using Application.Services.Interfaces.Mappers;
using Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Mappers
{
    public class RequestsMapper : IRequestsMapper
    {
        public IEnumerable<Request> ModelsToEntities(IEnumerable<RequestModel> models)
        {
            return models?.Select(x => new Request
            {
                Index = x.Id,
                Name = x.Name,
                Date = x.Date,
                Visits = x.Visits
            });
        }

        public IEnumerable<RequestModel> EntitiesToModels(IEnumerable<Request> entities)
        {
            return entities?.Select(x => new RequestModel
            {
                Id = x.Index,
                Name = x.Name,
                Date = x.Date,
                Visits = x.Visits
            });
        }
    }
}
