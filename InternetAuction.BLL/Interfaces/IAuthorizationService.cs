using System.Threading.Tasks;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Interfaces
{
    public interface IAuthorizationService
    {
        Task<LoggedInUserModel> SignInAsync(LoginModel model);

        Task<LoggedInUserModel> SignUpAsync(RegisterModel model);
    }
}