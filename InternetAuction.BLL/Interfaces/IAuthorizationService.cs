using System.Threading.Tasks;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Interfaces
{
    public interface IAuthorizationService
    {
        Task<TokenModel> SignInAsync(LoginModel model);

        Task<TokenModel> SignUpAsync(RegisterModel model);
    }
}