using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represents an image repository.
    /// </summary>
    public interface IImageRepository : ICrudRepository<Image>
    {
        /// <summary>
        /// Gets all images with includes.
        /// </summary>
        /// <returns>All images.</returns>
        Task<IEnumerable<Image>> GetAllWithDetailsAsync();

        /// <summary>
        /// Gets an image with specified id with includes.
        /// </summary>
        /// <param name="imageId">Image id.</param>
        /// <returns>An image with specified id.</returns>
        Task<Image> GetByIdWithDetailsAsync(int imageId);
    }
}