using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public async Task<TokenModel> SignInAsync(LoginModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TokenModel> SignUpAsync(RegisterModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}