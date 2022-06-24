using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using InternetAuction.API.Extensions;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Image;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.BLL.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController : ControllerBase
    {
        private readonly ILotService _lotService;
        private readonly IImageService _imageService;

        public LotsController(ILotService lotService, IImageService imageService)
        {
            _lotService = lotService;
            _imageService = imageService;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IEnumerable<LotCategoryModel>>> GetCategories()
        {
            return Ok(await _lotService.GetAllCategoriesAsync());
        }

        [HttpGet]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetLotsByCategory(int categoryId, [FromQuery] LotParameters lotParams)
        {
            var lots = await _lotService.GetLotsPreviewsByCategoryAsync(categoryId, lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        [HttpGet(Name = "GetById")]
        [Route("previews")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetLotsPreviews([FromQuery] LotParameters lotParams)
        {
            var lots = await _lotService.GetLotsPreviewsAsync(lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        [HttpGet]
        [Route("{lotId}")]
        public async Task<ActionResult<LotModel>> GetLotById(int lotId)
        {
            var lot = await _lotService.GetByIdWithDetailsAsync(lotId);

            if (lot is null)
            {
                return NotFound();
            }

            return Ok(lot);
        }

        [HttpPost]
        public async Task<ActionResult<LotModel>> AddLot(LotCreateModel model)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var created = await _lotService.AddAsync(model, userName);

            return CreatedAtRoute("GetById", new { id = created.Id }, created);
        }

        [HttpPost]
        [Route("{lotId}/image")]
        public async Task<ActionResult<ImageModel>> AddLotImage(int lotId, IFormFile image)
        {
            var created = await _imageService.AddAsync(image, null, lotId);

            return CreatedAtRoute("GetById", new { Id = created.Id }, created);
        }
    }
}