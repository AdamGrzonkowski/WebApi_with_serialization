using Domain.Model.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces.Base
{
    /// <summary>
    /// Abstraction for base repository.
    /// </summary>
    /// <typeparam name="TEntity">Entity class type, deriving from BaseEntity</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Returns all entities.
        /// </summary>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Inserts new record.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        int Insert(TEntity entity);
    }
}
