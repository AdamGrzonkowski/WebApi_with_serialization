using Domain.Model.Base;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Domain.Services
{
    /// <summary>
    /// Abstraction layer over db context.
    /// </summary>
    public interface IDbContext
    {
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        void Dispose();
        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}
