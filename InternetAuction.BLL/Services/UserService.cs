using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Image;
using InternetAuction.BLL.Models.User;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace InternetAuction.BLL.Services
{
    ///<inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        /// <summary>
        /// Creates a new instance of the UserService.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="imageService"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public UserService(IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper,
            UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserModel>> GetAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetByIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateAsync(string userName, UserUpdateModel model)
        {
            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);
            var userToUpdate = _mapper.Map(model, user);

            _unitOfWork.UserRepository.Update(userToUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllWithDetailsAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetByUserNameAsync(string userName)
        {
            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> GetByUserNameWithDetailsAsync(string userName)
        {
            var user = await _unitOfWork.UserRepository.GetByUserNameWithDetailsAsync(userName);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<ImageModel> SetProfileImage(string userName, IFormFile image)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

            if (currentUser.ProfileImage != null)
            {
                await _imageService.DeleteAsync(currentUser.ProfileImage.PublicId);
            }

            return await _imageService.AddAsync(image, currentUser.Id, null);
        }

        public async Task AddToRoleAsync(string userName, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                throw new ArgumentException("Specified role doesn't exist");
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                throw new ArgumentException("User with specified username doesn't exist.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.FirstOrDefault()
                                                  ?.Description);
            }
        }
    }
}