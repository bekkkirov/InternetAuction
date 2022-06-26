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
        /// <summary>
        /// Gets all bids with includes.
        /// </summary>
        /// <returns>All bids.</returns>
        Task<IEnumerable<Bid>> GetAllWithDetailsAsync();

        /// <summary>
        /// Gets a bid with specified id with includes. 
        /// </summary>
        /// <param name="bidId">Bid id.</param>
        /// <returns>A bid with specified id.</returns>
        Task<Bid> GetByIdWithDetailsAsync(int bidId);
    }
}