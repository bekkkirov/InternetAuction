using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
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
    }
}