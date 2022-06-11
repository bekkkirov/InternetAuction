using AutoMapper;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InternetAuction.BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorizationService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TokenModel> SignInAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null)
            {
                throw new ArgumentException("User with specified user name doesn't exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                throw new ArgumentException("Invalid password");
            }

            return new TokenModel() { Token = _tokenService.GenerateToken(user) };
        }

        public async Task<TokenModel> SignUpAsync(RegisterModel model)
        {
            var result = await _userManager.CreateAsync(new User() { UserName = model.UserName, Email = model.Email }, model.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException(string.Join($"{Environment.NewLine}", result.Errors.Select(e => e.Description)));
            }

            var user = _mapper.Map<AppUser>(model);
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            var identityUser = await _userManager.FindByNameAsync(model.UserName);

            return new TokenModel() { Token = _tokenService.GenerateToken(identityUser) };
        }
    }
}