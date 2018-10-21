using Domain.Model;
using Domain.Services.Interfaces;
using Domain.Services.Repositories.Base;

namespace Domain.Services.Repositories
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(IDbContext context) : base(context)
        {
        }
    }
}
