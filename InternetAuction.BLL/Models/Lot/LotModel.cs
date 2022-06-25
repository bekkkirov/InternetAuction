using System;
using System.Collections.Generic;
using InternetAuction.BLL.Models.Image;

namespace InternetAuction.BLL.Models.Lot
{
    public class LotModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public DateTime SaleEndTime { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public string SellerUserName { get; set; }

        public int? BuyerId { get; set; }

        public List<ImageModel> Images { get; set; }
    }
}