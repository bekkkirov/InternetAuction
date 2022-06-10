using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    public interface IUserRepository : ICrudRepository<AppUser>
    {
        Task<IEnumerable<AppUser>> GetAllWithDetailsAsync();

        Task<AppUser> GetByUserNameAsync(string userName);

        Task<AppUser> GetByUserNameWithDetailsAsync(string userName);
    }
}