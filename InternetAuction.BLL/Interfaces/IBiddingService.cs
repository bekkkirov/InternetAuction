using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Interfaces
{
    public interface IBiddingService
    {
        Task<IEnumerable<BidModel>> GetAsync();

        Task<BidModel> GetByIdAsync(int modelId);

        Task AddAsync(BidCreateModel model);

        Task DeleteByIdAsync(int modelId);

        Task<IEnumerable<BidModel>> GetAllWithDetailsAsync();

        Task<BidModel> GetByIdWithDetailsAsync(int modelId);
    }
}