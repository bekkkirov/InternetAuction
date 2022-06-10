using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.DAL.Repositories
{
    public class LotCategoryRepository : BaseRepository<LotCategory>, ILotCategoryRepository
    {
        public LotCategoryRepository(AuctionContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LotCategory>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(c => c.Lots)
                         .ToListAsync();
        }

        public async Task<LotCategory> GetByIdWithDetailsAsync(int categoryId)
        {
            return await _dbSet.Include(c => c.Lots)
                               .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}