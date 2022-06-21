using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Pagination;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.Interfaces
{
    public interface ILotService
    {
        Task<PagedList<LotPreviewModel>> GetLotsPreviewsAsync(LotParameters lotParams);

        Task<LotModel> GetByIdWithDetailsAsync(int lotId);

        Task<PagedList<LotPreviewModel>> GetLotsPreviewsByCategoryAsync(int categoryId, LotParameters lotParams);

        Task AddAsync(LotCreateModel model, int sellerId);

        Task DeleteByIdAsync(int modelId);

        Task<IEnumerable<LotModel>> SearchAsync(string searchValue);

        Task<IEnumerable<LotCategoryModel>> GetAllCategoriesAsync();

        Task AddCategoryAsync(LotCategoryCreateModel model);

        Task DeleteCategoryByIdAsync(int categoryId);
    }
}