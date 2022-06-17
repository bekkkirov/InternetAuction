using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.API.Extensions;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController : ControllerBase
    {
        private readonly ILotService _lotService;

        public LotsController(ILotService lotService)
        {
            _lotService = lotService;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IEnumerable<LotCategoryModel>>> GetCategories()
        {
            return Ok(await _lotService.GetAllCategoriesAsync());
        }

        [HttpGet]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetLotsByCategory(int categoryId, [FromQuery] LotPaginationParameters lotParams)
        {
            var lots = await _lotService.GetLotsByCategoryAsync(categoryId, lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        [HttpGet]
        [Route("previews")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetLotsPreviews([FromQuery] LotPaginationParameters paginationParams)
        {
            var lots = await _lotService.GetLotsPreviewsAsync(paginationParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }
    }
}