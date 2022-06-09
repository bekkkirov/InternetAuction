using System;
using System.Collections.Generic;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    public class Lot : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal InitialPrice { get; set; }

        public DateTime SaleStartTime { get; set; }

        public DateTime SaleEndTime { get; set; }

        public LotStatus Status { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public LotCategory Category { get; set; }

        public List<Image> Images { get; set; }

        public List<Bid> Bids { get; set; }

        public int SellerId { get; set; }
        public AppUser Seller { get; set; }

        public int? BuyerId { get; set; }
        public AppUser Buyer { get; set; }
    }
}