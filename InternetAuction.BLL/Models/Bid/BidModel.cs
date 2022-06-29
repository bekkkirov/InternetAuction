using System;

namespace InternetAuction.BLL.Models.Bid
{
    /// <summary>
    /// Represents a bid DTO.
    /// </summary>
    public class BidModel
    {
        public int Id { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }

        public int LotId { get; set; }

        public string LotName { get; set; }

        public int BidderId { get; set; }
    }
}