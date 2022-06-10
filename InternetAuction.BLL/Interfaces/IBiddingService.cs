using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Interfaces
{
    public interface IBiddingService : ICrudService<BidModel>
    {
        Task<IEnumerable<BidModel>> GetAllWithDetailsAsync();

        Task<BidModel> GetByIdWithDetailsAsync(int bidId);
    }
}