using System;

namespace InternetAuction.BLL.Models.Lot
{
    public class LotCreateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal InitialPrice { get; set; }

        public DateTime SaleEndTime { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
    }
}