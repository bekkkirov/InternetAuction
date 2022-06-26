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
        /// <summary>
        /// Gets all users with includes.
        /// </summary>
        /// <returns>All users.</returns>
        Task<IEnumerable<AppUser>> GetAllWithDetailsAsync();

        /// <summary>
        /// Gets a user with specified id with includes.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>A user with specified id.</returns>
        Task<AppUser> GetByIdWithDetailsAsync(int userId);

        /// <summary>
        /// Gets a user with specified username.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <returns>A user with specified username.</returns>
        Task<AppUser> GetByUserNameAsync(string userName);

        /// <summary>
        /// Gets a user with specified username with includes.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <returns>A user with specified username.</returns>
        Task<AppUser> GetByUserNameWithDetailsAsync(string userName);
    }
}