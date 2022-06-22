using System;

namespace InternetAuction.BLL.Models
{
    public class BidCreateModel
    {
        public decimal BidValue { get; set; }

        public int LotId { get; set; }

        public string BidderUserName { get; set; }
    }
}