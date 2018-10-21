using Domain.Services.Interfaces.Base;
using System;
using System.Linq;

namespace Domain.Services.Repositories.Base
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _context;

        public UnitOfWork(IDbContext context)
        {

            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Rollback()
        {
            _context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(async x => await x.ReloadAsync());
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