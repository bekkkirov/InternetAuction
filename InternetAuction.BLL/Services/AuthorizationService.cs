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
using InternetAuction.BLL.Exceptions;
using InternetAuction.BLL.Models.User;

namespace InternetAuction.BLL.Services
{
    ///<inheritdoc cref="IAuthorizationService"/>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates a new instance of the AuthorizationService.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="tokenService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public AuthorizationService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LoggedInUserModel> SignInAsync(LoginModel model)
        {
            var identityUser = await _userManager.FindByNameAsync(model.UserName);

            if (identityUser is null)
            {
                throw new SignInException("Invalid user name");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(identityUser, model.Password, false);

            if (!result.Succeeded)
            {
                throw new SignInException("Invalid password");
            }

            var user = await _unitOfWork.UserRepository.GetByUserNameWithDetailsAsync(model.UserName);

            return new LoggedInUserModel() { UserName = identityUser.UserName, Token = await _tokenService.GenerateTokenAsync(identityUser), ProfileImage = user.ProfileImage?.Url };
        }

        public async Task<LoggedInUserModel> SignUpAsync(RegisterModel model)
        {
            var result = await _userManager.CreateAsync(new User() { UserName = model.UserName, Email = model.Email }, model.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.FirstOrDefault()?.Description ?? "Something went wrong");
            }

            var userToAdd = _mapper.Map<AppUser>(model);
            _unitOfWork.UserRepository.Add(userToAdd);
            await _unitOfWork.SaveChangesAsync();

            var identityUser = await _userManager.FindByNameAsync(model.UserName);
            var user = await _unitOfWork.UserRepository.GetByUserNameWithDetailsAsync(identityUser.UserName);

            return new LoggedInUserModel() { UserName = identityUser.UserName, Token = await _tokenService.GenerateTokenAsync(identityUser)};
        }
    }
}