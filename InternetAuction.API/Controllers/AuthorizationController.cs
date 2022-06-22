using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<ActionResult<LoggedInUserModel>> SignIn(LoginModel model)
        {
            return Ok(await _authorizationService.SignInAsync(model));
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<ActionResult<LoggedInUserModel>> SignUp(RegisterModel model)
        {
            return Ok(await _authorizationService.SignUpAsync(model));
        }
    }
}