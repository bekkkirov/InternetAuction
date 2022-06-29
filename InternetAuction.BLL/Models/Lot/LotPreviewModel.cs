using System;
using InternetAuction.BLL.Models.Image;

namespace InternetAuction.BLL.Models.Lot
{
    /// <summary>
    /// Lot preview DTO.
    /// </summary>
    public class LotPreviewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime SaleEndTime { get; set; }

        public ImageModel Image { get; set; }

        public int BidCount { get; set; }

        public decimal CurrentPrice { get; set; }
    }
}