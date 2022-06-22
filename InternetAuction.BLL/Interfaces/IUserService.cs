using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.User;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAsync();

        Task<UserModel> GetByIdAsync(int modelId);

        Task UpdateAsync(string userName, UserUpdateModel model);

        Task<IEnumerable<UserModel>> GetAllWithDetailsAsync();

        Task<UserModel> GetByUserNameAsync(string userName);

        Task<UserModel> GetByUserNameWithDetailsAsync(string userName);
    }
}