using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Bid;

namespace InternetAuction.BLL.Interfaces
{
    public interface IBiddingService
    {
        Task<IEnumerable<BidModel>> GetAsync();

        Task<BidModel> GetByIdAsync(int modelId);

        Task<BidModel> AddAsync(BidCreateModel model, string userName, int lotId);

        Task DeleteByIdAsync(int modelId);
    }
}