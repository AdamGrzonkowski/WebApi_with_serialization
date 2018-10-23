using Application.Model;
using Domain.Model;
using System.Collections.Generic;

namespace Application.Services.Interfaces.Mappers
{
    public interface IRequestsMapper
    {
        IEnumerable<Request> ModelsToEntities(IEnumerable<RequestModel> models);
        IEnumerable<RequestModel> EntitiesToModels(IEnumerable<Request> entities);
    }
}
