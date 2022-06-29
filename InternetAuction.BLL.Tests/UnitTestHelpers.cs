using AutoMapper;
using InternetAuction.BLL.MapperConfigurations;
using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace InternetAuction.BLL.Tests
{
    public static class UnitTestHelpers
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });

            return config.CreateMapper();
        }

        public static Mock<UserManager<User>> CreateUserManagerMock()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            return userManagerMock;
        }

        public static Mock<RoleManager<UserRole>> CreateRoleManagerMock()
        {
            var roleStoreMock = new Mock<IRoleStore<UserRole>>();
            var roleManagerMock = new Mock<RoleManager<UserRole>>(roleStoreMock.Object, null, null, null, null);

            return roleManagerMock;
        }
    }
}