using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Services;
using InternetAuction.BLL.Settings;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace InternetAuction.BLL.Tests.Tests
{
    public class TokenServiceTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly IOptions<TokenSettings> _tokenSettings;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _userManagerMock = UnitTestHelpers.CreateUserManagerMock();
            _tokenSettings = Options.Create(new TokenSettings()
                { Key = "VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVerySecretKey" });

            _tokenService = new TokenService(_tokenSettings, _userManagerMock.Object);
        }

        #region TestData

        public static IList<string> UserRoles => new List<string>()
        {
            "User"
        };

        #endregion

        [Fact]
        public async Task GenerateToken_ShouldReturnToken()
        {
            // Arrange
            var user = new User() { Id = 1, UserName = "user" };

            _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                           .ReturnsAsync(UserRoles);

            // Act
            var actual = await _tokenService.GenerateTokenAsync(user);

            // Assert
            Assert.True(!string.IsNullOrEmpty(actual));
        }
    }
}