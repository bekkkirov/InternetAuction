using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    public interface IImageRepository : IReadWriteRepository<Image>
    {
        Task<IEnumerable<Image>> GetAllWithDetailsAsync();

        Task<Image> GetByIdWithDetailsAsync(int imageId);
    }
}