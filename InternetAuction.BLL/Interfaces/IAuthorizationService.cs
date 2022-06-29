using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.User;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents an authorization service.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Authorizes the user in the application.
        /// </summary>
        /// <param name="model">Login data.</param>
        /// <returns>Data of the authorized user.</returns>
        Task<LoggedInUserModel> SignInAsync(LoginModel model);

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="model">Register data.</param>
        /// <returns>Data of the newly registered user.</returns>
        Task<LoggedInUserModel> SignUpAsync(RegisterModel model);
    }
}