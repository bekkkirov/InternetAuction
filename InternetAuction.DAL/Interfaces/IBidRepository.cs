﻿using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.DAL.Entities;

namespace InternetAuction.DAL.Interfaces
{
    public interface IBidRepository : IReadWriteRepository<Bid>
    {
        Task<IEnumerable<Bid>> GetAllWithDetailsAsync();

        Task<Bid> GetByIdWithDetailsAsync(int bidId);
    }
}