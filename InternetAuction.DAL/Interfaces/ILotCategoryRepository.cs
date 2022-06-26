using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represents a lot category repository.
    /// </summary>
    public interface ILotCategoryRepository : ICrudRepository<LotCategory>
    {
        /// <summary>
        /// Gets all lot categories with includes.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LotCategory>> GetAllWithDetailsAsync();

        /// <summary>
        /// Gets a lot category with specified id with includes.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>A lot category with specified id.</returns>
        Task<LotCategory> GetByIdWithDetailsAsync(int categoryId);
    }
}