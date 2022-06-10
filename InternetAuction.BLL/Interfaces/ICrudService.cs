using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Interfaces
{
    public interface ICrudService<TModel> where TModel: class
    {
        Task<IEnumerable<TModel>> GetAsync();

        Task<TModel> GetByIdAsync(int modelId);

        void Add(TModel model);

        void Delete(TModel model);

        Task DeleteByIdAsync(int modelId);

        void Update(TModel model);
    }
}