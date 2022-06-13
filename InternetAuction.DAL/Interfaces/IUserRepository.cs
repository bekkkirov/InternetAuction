using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represent a lot repository.
    /// </summary>
    public interface IUserRepository : ICrudRepository<AppUser>
    {
        Task<IEnumerable<AppUser>> GetAllWithDetailsAsync();

        Task<AppUser> GetByIdWithDetailsAsync(int userId);

        Task<AppUser> GetByUserNameAsync(string userName);

        Task<AppUser> GetByUserNameWithDetailsAsync(string userName);
    }
}