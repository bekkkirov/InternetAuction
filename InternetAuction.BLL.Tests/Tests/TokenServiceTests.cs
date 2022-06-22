using System.Collections.Generic;
using System.Threading.Tasks;
using InternetAuction.BLL.Services;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace InternetAuction.BLL.Tests.Tests
{
    public class TokenServiceTests
    {
        public static IList<string> UserRoles => new List<string>()
        {
            "User"
        };

        [Fact]
        public async Task GenerateToken_ShouldReturnToken()
        {
            // Arrange
            var user = new User() {Id = 1, UserName = "user"};

            var tokenConfiguration = new Dictionary<string, string>()
            {
                { "Jwt:Key", "VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVerySecretKey" }
            };

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(tokenConfiguration)
                                                          .Build();

            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                           .ReturnsAsync(UserRoles);

            var tokenService = new TokenService(configuration, userManagerMock.Object);
            
            // Act
            var actual = await tokenService.GenerateTokenAsync(user);

            // Assert
            Assert.True(!string.IsNullOrEmpty(actual));
        }
    }
}