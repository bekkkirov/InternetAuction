using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Settings;
using InternetAuction.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetAuction.BLL.Models.Image;
using InternetAuction.DAL.Entities;

namespace InternetAuction.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public ImageService(IUnitOfWork unitOfWork, IOptions<CloudinarySettings> config, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinary = new Cloudinary(new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret));
        }

        public async Task<ImageModel> AddAsync(IFormFile file, int? userId, int? lotId)
        {
            if (file is null || file.Length == 0)
            {
                throw new ArgumentException("Image can't be empty");
            }

            ImageUploadResult uploadResult;

            await using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            if (uploadResult.Error != null)
            {
                throw new ArgumentException($"An error occurred while uploading an image: {uploadResult.Error.Message}");
            }

            var image = new Image()
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId
            };

            if (userId.HasValue)
            {
                var user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(userId.Value);
                user.ProfileImage = image;

                await _unitOfWork.SaveChangesAsync();
            }

            else if(lotId.HasValue)
            {
                var lot = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(lotId.Value);
                lot.Images.Add(image);

                await _unitOfWork.SaveChangesAsync();
            }

            return _mapper.Map<ImageModel>(image);
        }


        public async Task DeleteAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var deletionResult = await _cloudinary.DestroyAsync(deleteParams);

            if (deletionResult.Error != null)
            {
                throw new ArgumentException($"An error occurred while uploading an image: {deletionResult.Error.Message}");
            }

            var image = (await _unitOfWork.ImageRepository.GetAllWithDetailsAsync()).FirstOrDefault(i => i.PublicId.Equals(publicId));

            _unitOfWork.ImageRepository.Delete(image);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}