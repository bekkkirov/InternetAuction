﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Pagination;
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

        public async Task<PagedList<LotPreviewModel>> GetLotsByCategoryAsync(int categoryId, LotParameters lotParams)
        {
            var lots = await _unitOfWork.LotRepository.GetPreviewsByCategoryIdAsync(categoryId);
            var filteredLots = FilterLotsByParams(lots, lotParams);
            var mappedLots = _mapper.Map<IEnumerable<LotPreviewModel>>(filteredLots);

            return PagedList<LotPreviewModel>.CreateAsync(mappedLots, lotParams.PageNumber, lotParams.PageSize);
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

        public async Task<PagedList<LotPreviewModel>> GetLotsPreviewsAsync(LotParameters lotParams)
        {
            var lots = await _unitOfWork.LotRepository.GetPreviewsAsync();
            var filteredLots = FilterLotsByParams(lots, lotParams);

            var mappedLots = _mapper.Map<IEnumerable<LotPreviewModel>>(filteredLots);

            return PagedList<LotPreviewModel>.CreateAsync(mappedLots, lotParams.PageNumber, lotParams.PageSize) ;
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

        private IEnumerable<Lot> FilterLotsByParams(IEnumerable<Lot> lots, LotParameters lotParams)
        {
            var result = lots.Where(l => (l.Bids.LastOrDefault()?.BidValue ?? l.InitialPrice) >= lotParams.MinPrice);

            if (lotParams.MaxPrice.HasValue)
            {
                result = result.Where(l => (l.Bids.LastOrDefault()?.BidValue ?? l.InitialPrice) <= lotParams.MaxPrice);
            }

            if (Enum.TryParse(typeof(OrderOptions), lotParams.OrderOptions, true, out var orderOptions))
            {
                switch (orderOptions)
                {
                    case OrderOptions.PriceAscending:
                        result = result.OrderBy(l => l.Bids.LastOrDefault()
                                                      ?.BidValue ?? l.InitialPrice);
                        break;

                    case OrderOptions.PriceDescending:
                        result = result.OrderByDescending(l => l.Bids.LastOrDefault()
                                                                ?.BidValue ?? l.InitialPrice);
                        break;

                    case OrderOptions.NumberOfBidsAscending:
                        result = result.OrderBy(l => l.Bids.Count);
                        break;

                    case OrderOptions.NumberOfBidsDescending:
                        result = result.OrderByDescending(l => l.Bids.Count);
                        break;
                }
            }

            return result;
        }
    }
}