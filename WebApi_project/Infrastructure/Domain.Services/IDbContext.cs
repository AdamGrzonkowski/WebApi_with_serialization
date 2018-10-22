using Domain.Model.Base;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Domain.Services
{
    /// <summary>
    /// Abstraction layer over db context.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
        DbEntityEntry Entry(object entity);
    }
}
