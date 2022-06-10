using InternetAuction.Identity.Entities;

namespace InternetAuction.BLL.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}