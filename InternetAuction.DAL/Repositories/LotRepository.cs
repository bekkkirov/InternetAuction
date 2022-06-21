using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.DAL.Repositories
{
    public class LotRepository : BaseRepository<Lot>, ILotRepository
    {
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
            return await _dbSet.Where(l => EF.Functions.Like(l.Name, $"%{searchValue}%"))
                               .ToListAsync();
        }
    }
}