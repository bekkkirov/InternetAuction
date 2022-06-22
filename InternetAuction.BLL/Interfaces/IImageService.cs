﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Image;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.BLL.Interfaces
{
    public interface IImageService
    {
        Task<ImageModel> AddAsync(IFormFile file, int? userId, int? lotId);

        Task DeleteAsync(string publicId);
    }
}