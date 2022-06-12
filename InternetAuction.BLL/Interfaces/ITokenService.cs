using System.Threading.Tasks;
using InternetAuction.Identity.Entities;

namespace InternetAuction.BLL.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateTokenAsync(User user);
    }
}