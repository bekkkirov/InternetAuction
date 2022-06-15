using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.BLL.Services
{
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LotModel>> GetAsync()
        {
            var lots = await _unitOfWork.LotRepository.GetAsync();

            return _mapper.Map<IEnumerable<LotModel>>(lots);
        }

        public async Task<LotModel> GetByIdAsync(int modelId)
        {
            var lot = await _unitOfWork.LotRepository.GetByIdAsync(modelId);

            return _mapper.Map<LotModel>(lot);
        }

        public async Task AddAsync(LotCreateModel model)
        {
            var lotToAdd = _mapper.Map<Lot>(model);

            _unitOfWork.LotRepository.Add(lotToAdd);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(LotModel model)
        {
            var lotToDelete = _mapper.Map<Lot>(model);

            _unitOfWork.LotRepository.Delete(lotToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _unitOfWork.LotRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(LotModel model)
        {
            var lotToUpdate = _mapper.Map<Lot>(model);

            _unitOfWork.LotRepository.Update(lotToUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<LotModel>> GetAllWithDetailsAsync()
        {
            return _mapper.Map<IEnumerable<LotModel>>(await _unitOfWork.LotRepository.GetAllWithDetailsAsync());
        }

        public async Task<LotModel> GetByIdWithDetailsAsync(int lotId)
        {
            return _mapper.Map<LotModel>(await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(lotId));
        }

        public async Task<IEnumerable<LotModel>> SearchAsync(string searchValue)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<LotCategoryModel>> GetAllCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<LotCategoryModel>>(await _unitOfWork.LotCategoryRepository.GetAsync());
        }

        public async Task<LotCategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddCategoryAsync(LotCategoryModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteCategoryAsync(LotCategoryModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteCategoryByIdAsync(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateCategoryAsync(LotCategoryModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}