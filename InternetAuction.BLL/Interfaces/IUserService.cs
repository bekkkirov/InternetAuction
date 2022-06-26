using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.User;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents a user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>All users.</returns>
        Task<IEnumerable<UserModel>> GetAsync();

        /// <summary>
        /// Gets a user with specified id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>A user with specified id.</returns>
        Task<UserModel> GetByIdAsync(int userId);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="model">Update data.</param>
        Task UpdateAsync(string userName, UserUpdateModel model);

        /// <summary>
        /// Get all users with includes.
        /// </summary>
        /// <returns>All users.</returns>
        Task<IEnumerable<UserModel>> GetAllWithDetailsAsync();

        /// <summary>
        /// Gets a user by username.
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>A user with specified username.</returns>
        Task<UserModel> GetByUserNameAsync(string userName);

        /// <summary>
        /// Gets a user with specified username with includes.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <returns>A user with specified username.</returns>
        Task<UserModel> GetByUserNameWithDetailsAsync(string userName);
    }
}