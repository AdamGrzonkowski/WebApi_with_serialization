using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IRequestsService
    {
        Task WriteRequestsToFilesAsync(string directoryToSave);
        Task<int> SaveRequestsToDbAsync(IEnumerable<Request> requests);
    }
}
