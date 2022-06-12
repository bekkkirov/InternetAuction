using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageModel>> GetAsync()
        {
            var images = await _unitOfWork.ImageRepository.GetAsync();

            return _mapper.Map<IEnumerable<ImageModel>>(images);
        }

        public async Task<ImageModel> GetByIdAsync(int modelId)
        {
            var image = await _unitOfWork.ImageRepository.GetByIdAsync(modelId);

            if (image is null)
            {
                throw new ArgumentException("Image with specified id not found.");
            }

            return _mapper.Map<ImageModel>(image);
        }

        public async Task AddAsync(ImageModel model)
        {
            var imageToAdd = _mapper.Map<Image>(model);

            _unitOfWork.ImageRepository.Add(imageToAdd);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(ImageModel model)
        {
            var imageToDelete = _mapper.Map<Image>(model);

            _unitOfWork.ImageRepository.Delete(imageToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _unitOfWork.ImageRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ImageModel model)
        {
            var imageToUpdate = _mapper.Map<Image>(model);

            _unitOfWork.ImageRepository.Update(imageToUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ImageModel>> GetAllWithDetailsAsync()
        {
            var images = await _unitOfWork.ImageRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<ImageModel>>(images);
        }

        public async Task<ImageModel> GetByIdWithDetailsAsync(int imageId)
        {
            var image = await _unitOfWork.ImageRepository.GetByIdWithDetailsAsync(imageId);

            return _mapper.Map<ImageModel>(image);
        }
    }
}