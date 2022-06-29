using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Image;
using InternetAuction.BLL.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    /// <summary>
    /// Represents a user controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region Get

        /// <summary>
        /// Gets user by username.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <returns>User with specified username.</returns>
        [HttpGet("{userName}", Name = "GetByUsername")]
        public async Task<ActionResult<UserModel>> GetByUsername(string userName)
        {
            var user = await _userService.GetByUserNameWithDetailsAsync(userName);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        #endregion

        #region Post

        /// <summary>
        /// Sets profile image for the current user.
        /// </summary>
        /// <param name="image">New image.</param>
        /// <returns>Created image.</returns>
        [HttpPost]
        [Route("edit/image")]
        public async Task<ActionResult<ImageModel>> AddProfileImage(IFormFile image)
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var created = await _userService.SetProfileImage(currentUserName, image);

            return CreatedAtRoute("GetByUsername", new { username = currentUserName }, created);
        }

        #endregion

        #region Put

        /// <summary>
        /// Updates current user info.
        /// </summary>
        /// <param name="model">Update data.</param>
        [HttpPut]
        [Route("edit/profile")]
        public async Task<ActionResult> Update(UserUpdateModel model)
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userService.UpdateAsync(currentUserName, model);

            return NoContent();
        }

        #endregion
    }
}