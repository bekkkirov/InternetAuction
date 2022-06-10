using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.Interfaces
{
    public interface ILotService : ICrudService<Lot>
    {
        Task<IEnumerable<LotModel>> GetAllWithDetailsAsync();

        Task<LotModel> GetByIdWithDetailsAsync(int lotId);

        Task<IEnumerable<LotModel>> SearchAsync(string searchValue);

        Task<IEnumerable<LotCategoryModel>> GetAllCategoriesAsync();

        Task<LotCategoryModel> GetCategoryByIdAsync(int categoryId);

        Task AddCategoryAsync(LotCategoryModel model);

        Task DeleteCategoryAsync(LotCategoryModel model);

        Task DeleteCategoryByIdAsync(int categoryId);

        Task UpdateCategoryAsync(LotCategoryModel model);
    }
}