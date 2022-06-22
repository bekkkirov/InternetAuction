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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public UsersController(IUserService userService, IImageService imageService)
        {
            _userService = userService;
            _imageService = imageService;
        }

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

        [HttpPut]
        [Route("edit/profile")]
        public async Task<ActionResult> Update(UserUpdateModel model)
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userService.UpdateAsync(currentUserName, model);

            return NoContent();
        }

        [HttpPost]
        [Route("edit/image")]
        public async Task<ActionResult<ImageModel>> AddProfileImage(IFormFile image)
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userService.GetByUserNameWithDetailsAsync(currentUserName);

            if (currentUser.ProfileImage != null)
            {
                await _imageService.DeleteAsync(currentUser.ProfileImage.PublicId);
            }

            var created = await _imageService.AddAsync(image, currentUser.Id, null);

            return CreatedAtRoute("GetByUsername", new {username = currentUserName}, created);
        }
    }
}