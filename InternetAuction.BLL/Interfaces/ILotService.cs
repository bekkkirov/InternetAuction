using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.BLL.Pagination;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents a lot service.
    /// </summary>
    public interface ILotService
    {
        /// <summary>
        /// Gets lot previews with specified parameters.
        /// </summary>
        /// <param name="lotParams">Lot parameters.</param>
        /// <returns>Lot previews.</returns>
        Task<PagedList<LotPreviewModel>> GetLotsPreviewsAsync(LotParameters lotParams);

        /// <summary>
        /// Gets a lot with specified id.
        /// </summary>
        /// <param name="lotId">Lot id.</param>
        /// <returns>A lot with specified id.</returns>
        Task<LotModel> GetByIdWithDetailsAsync(int lotId);

        /// <summary>
        /// Gets lot previews by category with specified parameters. 
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <param name="lotParams">Lot parameters.</param>
        /// <returns></returns>
        Task<PagedList<LotPreviewModel>> GetLotsPreviewsByCategoryAsync(int categoryId, LotParameters lotParams);

        /// <summary>
        /// Creates new lot.
        /// </summary>
        /// <param name="model">Lot create data.</param>
        /// <param name="sellerUserName">Seller username.</param>
        /// <returns>A newly created lot.</returns>
        Task<LotModel> AddAsync(LotCreateModel model, string sellerUserName);

        /// <summary>
        /// Deletes lot with specified id.
        /// </summary>
        /// <param name="modelId">Lot id</param>
        /// <returns></returns>
        Task DeleteByIdAsync(string userName, int modelId);

        /// <summary>
        /// Searches for lots.
        /// </summary>
        /// <param name="searchValue">Search value</param>
        /// <returns>Lot previews.</returns>
        Task<PagedList<LotPreviewModel>> SearchAsync(string searchValue, LotParameters lotParams);

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>All categories.</returns>
        Task<IEnumerable<LotCategoryModel>> GetAllCategoriesAsync();

        /// <summary>
        /// Creates new category.
        /// </summary>
        /// <param name="model">Create data.</param>
        Task AddCategoryAsync(LotCategoryCreateModel model);
    }
}