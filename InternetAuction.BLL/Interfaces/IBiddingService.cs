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

        Task AddAsync(BidCreateModel model);

        Task DeleteByIdAsync(int modelId);
    }
}