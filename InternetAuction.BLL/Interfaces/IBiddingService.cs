using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Bid;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents a bidding service.
    /// </summary>
    public interface IBiddingService
    {
        /// <summary>
        /// Gets all bids.
        /// </summary>
        /// <returns>All bids.</returns>
        Task<IEnumerable<BidModel>> GetAsync();

        /// <summary>
        /// Gets a bid with specified id.
        /// </summary>
        /// <param name="modelId">Bid id.</param>
        /// <returns>A bid with specified id.</returns>
        Task<BidModel> GetByIdAsync(int modelId);

        /// <summary>
        /// Creates a new bid.
        /// </summary>
        /// <param name="model">Bid create data.</param>
        /// <param name="userName">Bidder username.</param>
        /// <param name="lotId">Lot id.</param>
        /// <returns>A newly created bid.</returns>
        Task<BidModel> AddAsync(BidCreateModel model, string userName, int lotId);
    }
}