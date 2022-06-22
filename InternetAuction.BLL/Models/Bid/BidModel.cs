using System;

namespace InternetAuction.BLL.Models.Bid
{
    public class BidModel
    {
        public int Id { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }

        public int LotId { get; set; }

        public int BidderId { get; set; }
    }
}