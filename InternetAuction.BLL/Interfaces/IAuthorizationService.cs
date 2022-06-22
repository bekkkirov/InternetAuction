using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.User;

namespace InternetAuction.BLL.Interfaces
{
    public interface IAuthorizationService
    {
        Task<LoggedInUserModel> SignInAsync(LoginModel model);

        Task<LoggedInUserModel> SignUpAsync(RegisterModel model);
    }
}