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
        Task<IEnumerable<LotCategory>> GetAllWithDetailsAsync();

        Task<LotCategory> GetByIdWithDetailsAsync(int categoryId);
    }
}