namespace InternetAuction.BLL.Models.Bid
{
    public class BidCreateModel
    {
        public decimal BidValue { get; set; }

        public int LotId { get; set; }

        public string BidderUserName { get; set; }
    }
}