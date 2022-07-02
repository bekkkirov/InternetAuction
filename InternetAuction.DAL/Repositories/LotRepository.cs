using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.DAL.Repositories
{
    ///<inheritdoc cref="ILotRepository"/>
    public class LotRepository : BaseRepository<Lot>, ILotRepository
    {
        ///<inheritdoc/>
        public LotRepository(AuctionContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lot>> GetPreviewsAsync()
        {
            return await _dbSet
                         .Include(l => l.Images)
                         .Include(l => l.Bids)
                         .Where(l => l.SaleEndTime > DateTime.Now)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Lot>> GetPreviewsByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                         .Include(l => l.Images)
                         .Include(l => l.Bids)
                         .Where(l => l.CategoryId == categoryId && l.SaleEndTime > DateTime.Now)
                         .ToListAsync();
        }

        public async Task<IEnumerable<Lot>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(l => l.Category)
                         .Include(l => l.Images)
                         .Include(l => l.Buyer)
                         .Include(l => l.Seller)
                         .Include(l => l.Bids)
                         .ToListAsync();
        }

        public async Task<Lot> GetByIdWithDetailsAsync(int lotId)
        {
            return await _dbSet.Include(l => l.Category)
                               .Include(l => l.Images)
                               .Include(l => l.Buyer)
                               .Include(l => l.Seller)
                               .Include(l => l.Bids)
                               .FirstOrDefaultAsync(l => l.Id == lotId);
        }

        public async Task<IEnumerable<Lot>> SearchAsync(string searchValue)
        {
            return await _dbSet.Include(l => l.Images)
                               .Include(l => l.Bids)
                               .Where(l => l.SaleEndTime > DateTime.Now && EF.Functions.Like(l.Name, $"%{searchValue}%"))
                               .ToListAsync();
        }

        public async Task<IEnumerable<Lot>> SearchWithCategoryAsync(int categoryId, string searchValue)
        {
            return await _dbSet.Include(l => l.Images)
                               .Include(l => l.Bids)
                               .Where(l => l.SaleEndTime > DateTime.Now && l.CategoryId == categoryId && EF.Functions.Like(l.Name, $"%{searchValue}%"))
                               .ToListAsync();
        }

        public async Task SetWinners()
        {
            var endedLots = await _dbSet.Include(l => l.Bids)
                                                .ThenInclude(b => b.Bidder)
                                                .Include(b => b.Seller)
                                                .Where(l => l.SaleEndTime < DateTime.Now && !l.BuyerId.HasValue && l.Bids.Any())
                                                .ToListAsync();

            foreach (var lot in endedLots)
            {
                var winningBid = lot.Bids.OrderByDescending(b => b.BidValue)
                                         .First();

                var winnerId = winningBid.BidderId;
                lot.BuyerId = winnerId;
                lot.Seller.Balance += winningBid.BidValue;

                var otherBids = lot.Bids.Where(b => b.BidderId != winnerId).GroupBy(b => b.Bidder);

                foreach (var group in otherBids)
                {
                    group.Key.Balance += group.ToList()
                                              .Max(b => b.BidValue);
                }
            }
        }
    }
}