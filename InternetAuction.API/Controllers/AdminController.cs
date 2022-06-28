using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILotService _lotService;
        private readonly IUserService _userService;

        public AdminController(ILotService lotService, IUserService userService, UserManager<User> userManager)
        {
            _lotService = lotService;
            _userService = userService;
        }

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpPost]
        [Route("categories/create")]
        public async Task<ActionResult> CreateCategory(LotCategoryCreateModel model)
        {
            await _lotService.AddCategoryAsync(model);

            return NoContent();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut]
        [Route("users/assign-role")]
        public async Task<ActionResult> AssignRole(string userName, string roleName)
        {
            await _userService.AddToRoleAsync(userName, roleName);

            return NoContent();
        }
    }
}