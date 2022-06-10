using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    public interface ILotCategoryRepository : IReadWriteRepository<LotCategory>
    {
        Task<IEnumerable<LotCategory>> GetAllWithDetailsAsync();

        Task<LotCategory> GetByIdWithDetailsAsync(int categoryId);
    }
}