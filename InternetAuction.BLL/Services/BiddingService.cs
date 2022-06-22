﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.BLL.Services
{
    public class BiddingService : IBiddingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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

        public async Task AddAsync(BidCreateModel model)
        {
            var lot = await _unitOfWork.LotRepository.GetByIdWithDetailsAsync(model.LotId);
            var currentPrice = lot.Bids.Count == 0 ? lot.InitialPrice : lot.Bids.Max(b => b.BidValue);

            if (DateTime.Now > lot.SaleEndTime)
            {
                throw new InvalidOperationException("Lot is closed");
            }

            if (currentPrice - model.BidValue > 10)
            {
                throw new ArgumentException("Invalid bid value", nameof(model.BidValue));
            }

            var bid = _mapper.Map<Bid>(model);

            var bidder = await _unitOfWork.UserRepository.GetByUserNameAsync(model.BidderUserName);
            bid.Bidder = bidder;

            _unitOfWork.BidRepository.Add(bid);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _unitOfWork.BidRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BidModel>> GetAllWithDetailsAsync()
        {
            var bids = await _unitOfWork.BidRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<BidModel>>(bids);
        }

        public async Task<BidModel> GetByIdWithDetailsAsync(int modelId)
        {
            var bid = await _unitOfWork.BidRepository.GetByIdWithDetailsAsync(modelId);

            return _mapper.Map<BidModel>(bid);
        }
    }
}