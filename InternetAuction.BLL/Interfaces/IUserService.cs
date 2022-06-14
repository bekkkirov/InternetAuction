using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AppUserModel>> GetAsync();

        Task<AppUserModel> GetByIdAsync(int modelId);

        Task UpdateAsync(string userName, UserUpdateModel model);

        Task<IEnumerable<AppUserModel>> GetAllWithDetailsAsync();

        Task<AppUserModel> GetByUserNameAsync(string userName);

        Task<AppUserModel> GetByUserNameWithDetailsAsync(string userName);
    }
}