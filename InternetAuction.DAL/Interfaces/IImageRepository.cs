using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    public interface IImageRepository : ICrudRepository<Image>
    {
        Task<IEnumerable<Image>> GetAllWithDetailsAsync();

        Task<Image> GetByIdWithDetailsAsync(int imageId);
    }
}