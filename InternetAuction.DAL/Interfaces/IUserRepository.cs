using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<IEnumerable<AppUser>> GetAllWithDetailsAsync();

        Task<AppUser> GetByUserNameAsync();

        Task<AppUser> GetByUserNameWithDetailsAsync(string userName);
    }
}