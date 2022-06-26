using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Image;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.BLL.Interfaces
{
    /// <summary>
    /// Represents an image service.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Adds an image.
        /// </summary>
        /// <param name="file">Image.</param>
        /// <param name="userId">User id.</param>
        /// <param name="lotId">Lot id.</param>
        /// <returns>Created image.</returns>
        Task<ImageModel> AddAsync(IFormFile file, int? userId, int? lotId);

        /// <summary>
        /// Deletes an image.
        /// </summary>
        /// <param name="publicId">Public id of the image</param>
        Task DeleteAsync(string publicId);
    }
}