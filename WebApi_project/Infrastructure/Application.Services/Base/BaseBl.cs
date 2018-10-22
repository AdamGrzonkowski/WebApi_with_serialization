using Domain.Services.Interfaces.Base;
using System;
using System.Threading.Tasks;

namespace Application.Services.Base
{
    public abstract class BaseBl
    {
        private readonly IUnitOfWork _unitOfWork;

        protected BaseBl(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        protected async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _unitOfWork.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }
    }
}
