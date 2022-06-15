using System;

namespace InternetAuction.BLL.Models
{
    public class LotPreviewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime SaleEndTime { get; set; }

        public ImageModel Image { get; set; }

        public int BidCount { get; set; }

        public int CurrentPrice { get; set; }
    }
}