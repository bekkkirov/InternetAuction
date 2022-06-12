using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    public interface ICrudService<TModel> where TModel: class
    {
        Task<IEnumerable<TModel>> GetAsync();

        Task<TModel> GetByIdAsync(int modelId);

        Task AddAsync(TModel model);

        Task DeleteAsync(TModel model);

        Task DeleteByIdAsync(int modelId);

        Task UpdateAsync(TModel model);
    }
}