using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InternetAuction.API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage,
            int totalItems, int totalPages)
        {
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(new {currentPage, itemsPerPage, totalItems, totalPages}));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}