using System;

namespace Domain.Services.Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves all pending changes.
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();

        /// <summary>
        /// Rolls back all pending changes and reloads objects with their values from db.
        /// </summary>
        void Rollback();
    }
}
