using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using InternetAuction.BLL.Models;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.BLL.Interfaces
{
    public interface IImageService
    {
        Task AddAsync(IFormFile file, int? userId, int? lotId);

        Task DeleteAsync(string publicId);
    }
}