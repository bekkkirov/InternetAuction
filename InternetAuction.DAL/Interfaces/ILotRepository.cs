using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represents a lot repository.
    /// </summary>
    public interface ILotRepository : ICrudRepository<Lot>
    {
        /// <summary>
        /// Gets all lots with includes.
        /// </summary>
        /// <returns>All lots.</returns>
        Task<IEnumerable<Lot>> GetAllWithDetailsAsync();

        /// <summary>
        /// Gets a lot with specified id with includes.
        /// </summary>
        /// <param name="lotId">Lot id.</param>
        /// <returns>A lot with specified id.</returns>
        Task<Lot> GetByIdWithDetailsAsync(int lotId);

        /// <summary>
        /// Gets lot previews by search value.
        /// </summary>
        /// <param name="searchValue">Search value.</param>
        /// <returns>Lot previews.</returns>
        Task<IEnumerable<Lot>> SearchAsync(string searchValue);

        /// <summary>
        /// Gets lot previews.
        /// </summary>
        /// <returns>Lot previews.</returns>
        Task<IEnumerable<Lot>> GetPreviewsAsync();

        /// <summary>
        /// Gets lot previews by category.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <returns>Lot previews that belong to specified category.</returns>
        Task<IEnumerable<Lot>> GetPreviewsByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Gets lot previews with specified category id and search value.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        Task<IEnumerable<Lot>> SearchWithCategoryAsync(int categoryId, string searchValue);

        /// <summary>
        /// Sets winners for all passed auctions.
        /// </summary>
        /// <returns></returns>
        Task SetWinners();
    }
}