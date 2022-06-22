using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InternetAuction.BLL.Exceptions;
using InternetAuction.BLL.Interfaces;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Services;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using InternetAuction.DAL.Repositories;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace InternetAuction.BLL.Tests.Tests
{
    public class AuthorizationServiceTests
    {
        #region TestData

        private static IEnumerable<AppUser> UserEntities => new List<AppUser>()
        {
            new AppUser()
            {
                UserName = "user1"
            },

            new AppUser()
            {
                UserName = "user2"
            }
        };

        private static IEnumerable<User> UserIdentities => new List<User>()
        {
            new User()
            {
                UserName = "user1"
            },

            new User()
            {
                UserName = "user2"
            }
        };

        #endregion

        private readonly Mock<SignInManager<User>> _signInManagerMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITokenService> _tokenServiceMock;

        private readonly AuthorizationService _authorizationService;

        public AuthorizationServiceTests()
        {
            _userManagerMock = UnitTestHelpers.CreateUserManagerMock();

            _signInManagerMock = new Mock<SignInManager<User>>(_userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object);

            _mapper = UnitTestHelpers.CreateMapper();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _tokenServiceMock = new Mock<ITokenService>();

            _authorizationService = new AuthorizationService(_userManagerMock.Object, _signInManagerMock.Object,
                _tokenServiceMock.Object, _unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task SignIn_ShouldThrowWhenLoginInvalid()
        {
            var loginModel = new LoginModel() { UserName = "Invalid userName", Password = "password" };

            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                            .ReturnsAsync(default(User));

            await Assert.ThrowsAsync<SignInException>(async () => await _authorizationService.SignInAsync(loginModel));
        }

        [Fact]
        public async Task SignIn_ShouldThrowWhenPasswordInvalid()
        {
            var loginModel = new LoginModel() { UserName = "user1", Password = "InvalidPassword" };

            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                            .ReturnsAsync((string userName) => UserIdentities.FirstOrDefault(u => u.UserName == userName));

            _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>()))
                              .ReturnsAsync(SignInResult.Failed);

            await Assert.ThrowsAsync<SignInException>(async () => await _authorizationService.SignInAsync(loginModel));
        }

        [Fact]
        public async Task SignIn_ShouldReturnCorrectModel()
        {
            // Arrange
            var loginModel = new LoginModel() { UserName = "user1", Password = "TotallyValidPassword" };

            _unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameWithDetailsAsync(It.IsAny<string>()))
                           .ReturnsAsync( (string userName) => UserEntities.FirstOrDefault(u => u.UserName == userName));

            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                            .ReturnsAsync( (string userName) => UserIdentities.FirstOrDefault(u => u.UserName == userName));

            _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>()))
                              .ReturnsAsync(SignInResult.Success);

            // Act
            var actual = await _authorizationService.SignInAsync(loginModel);

            // Assert
            Assert.Equal(loginModel.UserName, actual.UserName);
        }

        [Fact]
        public async Task SignUp_ShouldThrowWithInvalidData()
        {
            var registerModel = new RegisterModel() { UserName = "user1", Password = "password" };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Failed());
            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                            .ReturnsAsync((string userName) => UserIdentities.FirstOrDefault(u => u.UserName == userName));

            _unitOfWorkMock.Setup(x => x.UserRepository.Add(It.IsAny<AppUser>()));
            _unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameWithDetailsAsync(It.IsAny<string>()))
                           .ReturnsAsync((string userName) => UserEntities.FirstOrDefault(u => u.UserName == userName));

            await Assert.ThrowsAsync<ArgumentException>(async () => await _authorizationService.SignUpAsync(registerModel));
        }

        [Fact]
        public async Task SignUp_ShouldSignUpWithCorrectData()
        {
            // Arrange
            var registerModel = new RegisterModel() {UserName = "user1", Password = "password"};

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                            .ReturnsAsync( (string userName) => UserIdentities.FirstOrDefault(u => u.UserName == userName));

            _unitOfWorkMock.Setup(x => x.UserRepository.Add(It.IsAny<AppUser>()));
            _unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameWithDetailsAsync(It.IsAny<string>()))
                           .ReturnsAsync((string userName) => UserEntities.FirstOrDefault(u => u.UserName == userName));

            // Act
            var actual = await _authorizationService.SignUpAsync(registerModel);

            // Assert
            _unitOfWorkMock.Verify(x => x.UserRepository.Add(It.Is<AppUser>(u => u.UserName == registerModel.UserName)), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            Assert.True(!string.IsNullOrEmpty(actual.UserName));
        }
    }
}