namespace InternetAuction.BLL.Pagination
{
    public class LotPaginationParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; } = 10;
    }
}