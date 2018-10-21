using Domain.Model.Base;
using Domain.Services.Interfaces.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Domain.Services.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IDbContext _context;

        protected BaseRepository(IDbContext context)
        {
            _context = context;
        }

        private IDbSet<TEntity> Entities => _context.Set<TEntity>();

        public async Task<List<TEntity>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public int Insert(TEntity entity)
        {
            return Entities.Add(entity).Index;
        }
    }
}
