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
        Task<IEnumerable<Lot>> GetAllWithDetailsAsync();

        Task<Lot> GetByIdWithDetailsAsync(int lotId);

        Task<IEnumerable<Lot>> SearchAsync(string searchValue);

        Task<IEnumerable<Lot>> GetPreviewsAsync();

        Task<IEnumerable<Lot>> GetPreviewsByCategoryIdAsync(int categoryId);
    }
}