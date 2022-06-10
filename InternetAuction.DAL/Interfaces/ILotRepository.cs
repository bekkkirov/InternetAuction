using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    public interface ILotRepository : IReadWriteRepository<Lot>
    {
        Task<IEnumerable<Lot>> GetAllWithDetailsAsync();

        Task<Lot> GetByIdWithDetailsAsync(int lotId);

        Task<IEnumerable<Lot>> Search(string searchValue);
    }
}