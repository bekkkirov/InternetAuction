using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using InternetAuction.API.Extensions;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Bid;
using InternetAuction.BLL.Models.Image;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.BLL.Pagination;
using InternetAuction.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    /// <summary>
    /// Represents a lots controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController : ControllerBase
    {
        private readonly ILotService _lotService;
        private readonly IImageService _imageService;
        private readonly IBiddingService _bidService;

        public LotsController(ILotService lotService, IImageService imageService, IBiddingService bidService)
        {
            _lotService = lotService;
            _imageService = imageService;
            _bidService = bidService;
        }

        #region Get

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>All categories.</returns>
        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IEnumerable<LotCategoryModel>>> GetCategories()
        {
            return Ok(await _lotService.GetAllCategoriesAsync());
        }

        /// <summary>
        /// Gets lots with specified category.
        /// </summary>
        /// <param name="categoryId">Category id.</param>
        /// <param name="lotParams">Lot parameters.</param>
        /// <returns>Lot previews.</returns>
        [Authorize]
        [HttpGet]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetByCategory(int categoryId, [FromQuery] LotParameters lotParams)
        {
            var lots = await _lotService.GetLotsPreviewsByCategoryAsync(categoryId, lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        /// <summary>
        /// Gets all lots.
        /// </summary>
        /// <param name="lotParams">Lot parameters</param>
        /// <returns>All lots.</returns>
        [HttpGet]
        [Route("previews")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetPreviews([FromQuery] LotParameters lotParams)
        {
            var lots = await _lotService.GetLotsPreviewsAsync(lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        /// <summary>
        /// Gets lot by with specified id.
        /// </summary>
        /// <param name="lotId">Lot id.</param>
        /// <returns>Lot with specified id.</returns>
        [Authorize]
        [HttpGet(Name = "GetById")]
        [Route("{lotId}")]
        public async Task<ActionResult<LotModel>> GetById(int lotId)
        {
            var lot = await _lotService.GetByIdWithDetailsAsync(lotId);

            if (lot is null)
            {
                return NotFound();
            }

            return Ok(lot);
        }

        #endregion

        #region Post

        /// <summary>
        /// Creates new lot.
        /// </summary>
        /// <param name="model">Create data.</param>
        /// <returns>Created lot.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LotModel>> Add(LotCreateModel model)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var created = await _lotService.AddAsync(model, userName);

            return CreatedAtRoute("GetById", new { id = created.Id }, created);
        }

        /// <summary>
        /// Adds image to the specified lot.
        /// </summary>
        /// <param name="lotId">Lot id.</param>
        /// <param name="image">Image.</param>
        /// <returns>Created image.</returns>
        [Authorize]
        [HttpPost]
        [Route("{lotId}/image")]
        public async Task<ActionResult<ImageModel>> AddLotImage(int lotId, IFormFile image)
        {
            var created = await _imageService.AddAsync(image, null, lotId);

            return CreatedAtRoute("GetById", new { Id = lotId }, created);
        }

        /// <summary>
        /// Places new bid for the specified lot.
        /// </summary>
        /// <param name="model">Create data.</param>
        /// <param name="lotId">Lot id.</param>
        /// <returns>Created bid.</returns>
        [Authorize]
        [HttpPost]
        [Route("{lotId}/bids")]
        public async Task<ActionResult<BidModel>> PlaceBid(BidCreateModel model, int lotId)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var created = await _bidService.AddAsync(model, userName, lotId);

            return CreatedAtRoute("GetById", new { Id = lotId }, created);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes lot with specified id.
        /// </summary>
        /// <param name="lotId">Lot id.</param>
        [Authorize]
        [HttpDelete]
        [Route("{lotId}")]
        public async Task<ActionResult> DeleteLot(int lotId)
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _lotService.DeleteByIdAsync(currentUserName, lotId);

            return NoContent();
        }

        #endregion
    }
}