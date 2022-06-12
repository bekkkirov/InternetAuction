using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represents the base repository.
    /// </summary>
    /// <typeparam name="TEntity">Repository entity.</typeparam>
    public interface ICrudRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Enumeration of all entities of this type.</returns>
        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// Gets an entity by id.
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <returns>Entity with specified id.</returns>
        Task<TEntity> GetByIdAsync(int entityId);

        /// <summary>
        /// Adds new entity.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity by id.
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(int entityId);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(TEntity entity);
    }
}