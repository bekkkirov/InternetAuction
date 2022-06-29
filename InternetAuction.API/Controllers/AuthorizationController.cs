using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    /// <summary>
    /// Represents an authorization controller.
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        #region Post

        /// <summary>
        /// Authorizes the user in the application.
        /// </summary>
        /// <param name="model">Login data.</param>
        /// <returns>Data of the authorized user.</returns>
        [HttpPost]
        [Route("sign-in")]
        public async Task<ActionResult<LoggedInUserModel>> SignIn(LoginModel model)
        {
            return Ok(await _authorizationService.SignInAsync(model));
        }

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="model">Register data.</param>
        /// <returns>Data of the created user.</returns>
        [HttpPost]
        [Route("sign-up")]
        public async Task<ActionResult<LoggedInUserModel>> SignUp(RegisterModel model)
        {
            return Ok(await _authorizationService.SignUpAsync(model));
        }

        #endregion




    }
}