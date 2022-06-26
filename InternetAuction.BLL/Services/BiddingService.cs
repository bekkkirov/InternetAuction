using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Bid;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.BLL.Services
{
    ///<inheritdoc cref="IBiddingService"/>
    public class BiddingService : IBiddingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the BiddingService.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public BiddingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BidModel>> GetAsync()
        {
            var bids = await _unitOfWork.BidRepository.GetAsync();

            return _mapper.Map<IEnumerable<BidModel>>(bids);
        }

        public async Task<BidModel> GetByIdAsync(int modelId)
        {
            var bid = await _unitOfWork.BidRepository.GetByIdAsync(modelId);

            return _mapper.Map<BidModel>(bid);
        }

        public async Task<BidModel> AddAsync(BidCreateModel model, string userName, int lotId)
        {
            var lot = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(lotId);

            if (lot.Seller.UserName == userName)
            {
                throw new InvalidOperationException("Bidding on your own lot is not possible");
            }

            var currentPrice = lot.Bids.Count == 0 ? lot.InitialPrice : lot.Bids.Max(b => b.BidValue);

            if (DateTime.Now > lot.SaleEndTime)
            {
                throw new InvalidOperationException("Lot is closed");
            }

            if (model.BidValue < currentPrice || model.BidValue - currentPrice < 5)
            {
                throw new ArgumentException("Bid value is too low");
            }

            var bid = _mapper.Map<Bid>(model);

            var bidder = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);
            bid.Bidder = bidder;
            bid.Lot = lot;

            _unitOfWork.BidRepository.Add(bid);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BidModel>(bid);
        }
    }
}