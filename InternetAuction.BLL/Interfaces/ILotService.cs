using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Pagination;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.Interfaces
{
    public interface ILotService
    {
        Task<IEnumerable<LotModel>> GetAllWithDetailsAsync();

        Task<PagedList<LotPreviewModel>> GetLotsPreviewsAsync(LotPaginationParameters paginationParams);

        Task<LotModel> GetByIdWithDetailsAsync(int lotId);

        Task AddAsync(LotCreateModel model);

        Task DeleteByIdAsync(int modelId);

        Task<IEnumerable<LotModel>> SearchAsync(string searchValue);

        Task<IEnumerable<LotCategoryModel>> GetAllCategoriesAsync();

        Task<LotCategoryModel> GetCategoryByIdAsync(int categoryId);

        Task AddCategoryAsync(LotCategoryModel model);

        Task DeleteCategoryByIdAsync(int categoryId);

        Task UpdateCategoryAsync(LotCategoryModel model);
    }
}