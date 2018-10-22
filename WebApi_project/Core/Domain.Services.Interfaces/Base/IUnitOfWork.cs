using System;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all pending changes.
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Rolls back all pending changes and reloads objects with their values from db.
        /// </summary>
        Task RollbackAsync();
    }
}
