using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Interfaces
{
    public interface IUserService : ICrudService<AppUserModel>
    {
        Task<IEnumerable<AppUserModel>> GetAllWithDetailsAsync();

        Task<AppUserModel> GetByUserNameAsync(string userName);

        Task<AppUserModel> GetByUserNameWithDetailsAsync(string userName);
    }
}