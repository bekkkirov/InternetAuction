﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.User;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.BLL.Services
{
    ///<inheritdoc cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the UserService.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}