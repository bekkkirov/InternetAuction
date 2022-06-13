using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AppUserModel>> GetAsync();

        Task<AppUserModel> GetByIdAsync(int modelId);

        Task UpdateAsync(AppUserModel model);

        Task<IEnumerable<AppUserModel>> GetAllWithDetailsAsync();

        Task<AppUserModel> GetByUserNameAsync(string userName);

        Task<AppUserModel> GetByUserNameWithDetailsAsync(string userName);
    }
}