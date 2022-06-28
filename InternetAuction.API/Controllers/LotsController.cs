﻿using System.Collections.Generic;
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

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IEnumerable<LotCategoryModel>>> GetCategories()
        {
            return Ok(await _lotService.GetAllCategoriesAsync());
        }

        [Authorize]
        [HttpGet]
        [Route("categories/{categoryId}")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetLotsByCategory(int categoryId, [FromQuery] LotParameters lotParams)
        {
            var lots = await _lotService.GetLotsPreviewsByCategoryAsync(categoryId, lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        [HttpGet]
        [Route("previews")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> GetLotsPreviews([FromQuery] LotParameters lotParams)
        {
            var lots = await _lotService.GetLotsPreviewsAsync(lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        [Authorize]
        [HttpGet(Name = "GetById")]
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

        [Authorize]
        [HttpGet]
        [Route("search/{searchValue}")]
        public async Task<ActionResult<PagedList<LotPreviewModel>>> Search(string searchValue, [FromQuery] LotParameters lotParams)
        {
            var lots = await _lotService.SearchAsync(searchValue, lotParams);

            Response.AddPaginationHeader(lots.CurrentPage, lots.PageSize, lots.ItemsCount, lots.TotalPages);

            return Ok(lots);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LotModel>> AddLot(LotCreateModel model)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var created = await _lotService.AddAsync(model, userName);

            return CreatedAtRoute("GetById", new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPost]
        [Route("{lotId}/image")]
        public async Task<ActionResult<ImageModel>> AddLotImage(int lotId, IFormFile image)
        {
            var created = await _imageService.AddAsync(image, null, lotId);

            return CreatedAtRoute("GetById", new { Id = lotId }, created);
        }

        [Authorize]
        [HttpPost]
        [Route("{lotId}/bids")]
        public async Task<ActionResult<Bid>> PlaceBid(BidCreateModel model, int lotId)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var created = await _bidService.AddAsync(model, userName, lotId);

            return CreatedAtRoute("GetById", new {Id = lotId }, created);
        }

        [Authorize]
        [HttpDelete]
        [Route("{lotId}")]
        public async Task<ActionResult> DeleteLot(int lotId)
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _lotService.DeleteByIdAsync(currentUserName, lotId);

            return NoContent();
        }
    }
}