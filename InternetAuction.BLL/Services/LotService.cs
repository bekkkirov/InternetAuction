using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.BLL.Pagination;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.BLL.Services
{
    ///<inheritdoc cref="ILotService"/>
    public class LotService : ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the LotService.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
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

        public async Task<PagedList<LotPreviewModel>> GetLotsPreviewsByCategoryAsync(int categoryId, LotParameters lotParams)
        {
            IEnumerable<Lot> lots;

            if (lotParams.SearchValue != null && !string.IsNullOrWhiteSpace(lotParams.SearchValue))
            {
                lots = await _unitOfWork.LotRepository.SearchWithCategoryAsync(categoryId, lotParams.SearchValue);
            }

            else
            {
                lots = await _unitOfWork.LotRepository.GetPreviewsByCategoryIdAsync(categoryId);
            }

            var filteredLots = FilterLotsByParams(lots, lotParams);
            var mappedLots = _mapper.Map<IEnumerable<LotPreviewModel>>(filteredLots);

            return PagedList<LotPreviewModel>.CreateAsync(mappedLots, lotParams.PageNumber, lotParams.PageSize);
        }

        public async Task<LotModel> AddAsync(LotCreateModel model, string sellerUserName)
        {
            var lotToAdd = _mapper.Map<Lot>(model);
            var seller = await _unitOfWork.UserRepository.GetByUserNameAsync(sellerUserName);
            lotToAdd.Seller = seller;

            _unitOfWork.LotRepository.Add(lotToAdd);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LotModel>(lotToAdd);
        }

        public async Task DeleteByIdAsync(string userName, int modelId)
        {
            var lot = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(modelId);

            if (userName != lot.Seller.UserName)
            {
                throw new ArgumentException("You can't delete this lot");
            }

            if (lot.SaleEndTime <= DateTime.Now)
            {
                throw new ArgumentException("You can't delete an item after the sale end");
            }

            await _unitOfWork.LotRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedList<LotPreviewModel>> GetLotsPreviewsAsync(LotParameters lotParams)
        {
            await _unitOfWork.LotRepository.SetWinners();
            await _unitOfWork.SaveChangesAsync();

            IEnumerable<Lot> lots;

            if (lotParams.SearchValue != null && !string.IsNullOrWhiteSpace(lotParams.SearchValue))
            {
                lots = await _unitOfWork.LotRepository.SearchAsync(lotParams.SearchValue);
            }

            else
            {
                lots = await _unitOfWork.LotRepository.GetPreviewsAsync();
            }

            var filteredLots = FilterLotsByParams(lots, lotParams);
            var mappedLots = _mapper.Map<IEnumerable<LotPreviewModel>>(filteredLots);

            return PagedList<LotPreviewModel>.CreateAsync(mappedLots, lotParams.PageNumber, lotParams.PageSize);
        }

        public async Task<LotModel> GetByIdWithDetailsAsync(int lotId)
        {
            return _mapper.Map<LotModel>(await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(lotId));
        }

        public async Task<IEnumerable<LotCategoryModel>> GetAllCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<LotCategoryModel>>(await _unitOfWork.LotCategoryRepository.GetAsync());
        }

        public async Task AddCategoryAsync(LotCategoryCreateModel model)
        {
            var categoryToAdd = _mapper.Map<LotCategory>(model);

            _unitOfWork.LotCategoryRepository.Add(categoryToAdd);
            await _unitOfWork.SaveChangesAsync();
        }

        private IEnumerable<Lot> FilterLotsByParams(IEnumerable<Lot> lots, LotParameters lotParams)
        {
            var result = lots.Where(l => (l.Bids.LastOrDefault()?.BidValue ?? l.InitialPrice) >= lotParams.MinPrice);

            if (lotParams.MaxPrice.HasValue && lotParams.MaxPrice.Value > 0)
            {
                result = result.Where(l => (l.Bids.LastOrDefault()?.BidValue ?? l.InitialPrice) <= lotParams.MaxPrice);
            }

            if (Enum.TryParse(typeof(OrderOptions), lotParams.OrderOptions, true, out var orderOptions))
            {
                switch (orderOptions)
                {
                    case OrderOptions.PriceDescending:
                        return result.OrderByDescending(l => l.Bids.LastOrDefault()
                                                                ?.BidValue ?? l.InitialPrice);

                    case OrderOptions.BidsAscending:
                        return result.OrderBy(l => l.Bids.Count);

                    case OrderOptions.BidsDescending:
                        return result.OrderByDescending(l => l.Bids.Count);
                }
            }

            return result.OrderBy(l => l.Bids.LastOrDefault()
                                        ?.BidValue ?? l.InitialPrice);
        }
    }
}