using Domain.Model.Base;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Domain.Services
{
    /// <summary>
    /// Abstraction layer over db context.
    /// </summary>
    public interface IDbContext
    {
        DbChangeTracker ChangeTracker { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
