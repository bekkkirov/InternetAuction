using System;
using System.Collections.Generic;

namespace InternetAuction.BLL.Models
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

        public int SellerId { get; set; }

        public int? BuyerId { get; set; }

        public List<ImageModel> Images { get; set; }
    }
}