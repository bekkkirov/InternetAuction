using System;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    public class Bid : IEntity
    {
        public int Id { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }

        public int LotId { get; set; }
        public Lot Lot { get; set; }

        public int BidderId { get; set; }
        public AppUser Bidder { get; set; }
    }
}