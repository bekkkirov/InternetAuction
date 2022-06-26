using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.DAL.Repositories
{
    ///<inheritdoc cref="IImageRepository"/>
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        ///<inheritdoc/>
        public ImageRepository(AuctionContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Image>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(i => i.Lot)
                               .Include(i => i.User)
                               .ToListAsync();
        }

        public async Task<Image> GetByIdWithDetailsAsync(int imageId)
        {
            return await _dbSet.Include(i => i.Lot)
                               .Include(i => i.User)
                               .FirstOrDefaultAsync(i => i.Id == imageId);
        }
    }
}