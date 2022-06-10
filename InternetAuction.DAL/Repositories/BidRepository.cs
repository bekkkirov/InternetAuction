using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.DAL.Repositories
{
    public class BidRepository : BaseRepository<Bid>, IBidRepository
    {
        public BidRepository(AuctionContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Bid>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(b => b.Lot)
                               .Include(b => b.Bidder)
                               .ToListAsync();
        }

        public async Task<Bid> GetByIdWithDetailsAsync(int bidId)
        {
            return await _dbSet.Include(b => b.Lot)
                               .Include(b => b.Bidder)
                               .FirstOrDefaultAsync(b => b.Id == bidId);
        }

       
    }
}