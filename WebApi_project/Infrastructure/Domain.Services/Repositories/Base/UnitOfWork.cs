using Domain.Services.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services.Repositories.Base
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _context;

        public UnitOfWork(IDbContext context)
        {

            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false); ;
        }

        public async Task RollbackAsync()
        {
            List<DbEntityEntry> entriesToRollback = _context.ChangeTracker.Entries().ToList();
            foreach (DbEntityEntry entity in entriesToRollback)
            {
                // for newly added entities, we can't reload anything from db, as it's not there yet
                if (entity.State != EntityState.Added && entity.State != EntityState.Detached)
                {
                    await entity.ReloadAsync().ConfigureAwait(false);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}