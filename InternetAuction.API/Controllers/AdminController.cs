using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    /// <summary>
    /// Represents an admin controller.
    /// </summary>
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

        #region Post

        /// <summary>
        /// Creates new category.
        /// </summary>
        /// <param name="model">Create data.</param>
        [Authorize(Policy = "RequireModeratorRole")]
        [HttpPost]
        [Route("categories/create")]
        public async Task<ActionResult> CreateCategory(LotCategoryCreateModel model)
        {
            await _lotService.AddCategoryAsync(model);

            return NoContent();
        }

        #endregion

        #region Put

        /// <summary>
        /// Assigns new role to the specified user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="roleName">Role name.</param>
        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPut]
        [Route("users/assign-role")]
        public async Task<ActionResult> AssignRole(string userName, string roleName)
        {
            await _userService.AddToRoleAsync(userName, roleName);

            return NoContent();
        }

        #endregion
    }
}