using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.DAL.Repositories
{
    ///<inheritdoc cref="IUserRepository"/>
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        ///<inheritdoc/>
        public UserRepository(AuctionContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AppUser>> GetAllWithDetailsAsync()
        {
            return await _dbSet.Include(u => u.ProfileImage)
                               .Include(u => u.BoughtLots)
                               .Include(u => u.RegisteredLots)
                               .Include(u => u.Bids)
                               .ToListAsync();
        }

        public async Task<AppUser> GetByIdWithDetailsAsync(int userId)
        {
            return await _dbSet.Include(u => u.ProfileImage)
                               .Include(u => u.BoughtLots)
                               .Include(u => u.RegisteredLots)
                               .Include(u => u.Bids)
                               .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<AppUser> GetByUserNameWithDetailsAsync(string userName)
        {
            return await _dbSet.Include(u => u.ProfileImage)
                               .Include(u => u.BoughtLots)
                               .Include(u => u.RegisteredLots)
                               .Include(u => u.Bids)
                               .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}