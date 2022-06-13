using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetAuction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    }
}