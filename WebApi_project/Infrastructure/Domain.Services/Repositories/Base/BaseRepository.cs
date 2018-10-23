using Domain.Model.Base;
using Domain.Services.Interfaces.Base;
using System;
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

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync().ConfigureAwait(false);
        }

        public void Insert(TEntity entity)
        {
            entity.InsTs = DateTime.Now;
            Entities.Add(entity);
        }
    }
}
