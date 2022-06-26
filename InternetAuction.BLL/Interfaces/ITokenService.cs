using System.Threading.Tasks;
using InternetAuction.Identity.Entities;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents a token service.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a token for the specified user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>A token for the specified user.</returns>
        public Task<string> GenerateTokenAsync(User user);
    }
}