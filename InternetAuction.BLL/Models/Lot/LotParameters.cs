namespace InternetAuction.BLL.Models.Lot
{
    /// <summary>
    /// Represents a lot parameters DTO.
    /// </summary>
    public class LotParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; } = 10;

        public string SearchValue { get; set; }

        public decimal MinPrice { get; set; } = 0;

        public decimal? MaxPrice { get; set; }

        public string OrderOptions { get; set; }
    }
}