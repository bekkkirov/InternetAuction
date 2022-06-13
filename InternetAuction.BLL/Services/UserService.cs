using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUserModel>> GetAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAsync();

            return _mapper.Map<IEnumerable<AppUserModel>>(users);
        }

        public async Task<AppUserModel> GetByIdAsync(int modelId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(modelId);

            return _mapper.Map<AppUserModel>(user);
        }

        public async Task UpdateAsync(AppUserModel model)
        {
            var userToUpdate = _mapper.Map<AppUser>(model);

            _unitOfWork.UserRepository.Update(userToUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppUserModel>> GetAllWithDetailsAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<AppUserModel>>(users);
        }

        public async Task<AppUserModel> GetByUserNameAsync(string userName)
        {
            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);

            return _mapper.Map<AppUserModel>(user);
        }

        public async Task<AppUserModel> GetByUserNameWithDetailsAsync(string userName)
        {
            var user = await _unitOfWork.UserRepository.GetByUserNameWithDetailsAsync(userName);

            return _mapper.Map<AppUserModel>(user);
        }
    }
}