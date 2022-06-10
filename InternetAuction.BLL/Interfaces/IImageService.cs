using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Interfaces
{
    public interface IImageService : ICrudService<ImageModel>
    {
        Task<IEnumerable<ImageModel>> GetAllWithDetailsAsync();

        Task<ImageModel> GetByIdWithDetailsAsync(int imageId);
    }
}