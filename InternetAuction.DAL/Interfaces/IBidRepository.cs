using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represents a bid repository.
    /// </summary>
    public interface IBidRepository : ICrudRepository<Bid>
    {
        Task<IEnumerable<Bid>> GetAllWithDetailsAsync();

        Task<Bid> GetByIdWithDetailsAsync(int bidId);
    }
}