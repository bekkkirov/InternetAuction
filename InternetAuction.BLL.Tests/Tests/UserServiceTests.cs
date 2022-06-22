using InternetAuction.BLL.Models;
using InternetAuction.BLL.Services;
using InternetAuction.BLL.Tests.Comparers;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InternetAuction.BLL.Tests.Tests
{
    public class UserServiceTests
    {
        #region TestData

        private static IEnumerable<AppUser> UserEntities => new List<AppUser>()
            {
                new AppUser()
                {
                    Id = 1,
                    UserName = "user1",
                    FirstName = "FirstUser1",
                    LastName= "LastUser1",
                    Bids = new List<Bid>()
                    {

                    }
                },

                new AppUser()
                {
                    Id = 2,
                    UserName = "user2",
                    FirstName = "FirstUser2",
                    LastName= "LastUser2",
                },

                new AppUser()
                {
                    Id = 3,
                    UserName = "user3",
                    FirstName = "FirstUser3",
                    LastName= "LastUser3",
                    Bids = new List<Bid>()
                    {
                        new Bid()
                        {
                            Id = 1,
                            BidValue = 100
                        }
                    }
                },

                new AppUser()
                {
                    Id = 4,
                    UserName = "user4",
                    FirstName = "FirstUser4",
                    LastName= "LastUser4",
                    Bids = new List<Bid>()
                    {
                        new Bid()
                        {
                            Id = 2,
                            BidValue = 150
                        }
                    }
                },

                new AppUser()
                {
                    Id = 5,
                    UserName = "user5",
                    FirstName = "FirstUser5",
                    LastName= "LastUser5",
                },

                new AppUser()
                {
                    Id = 6,
                    UserName = "user6",
                    FirstName = "FirstUser6",
                    LastName= "LastUser6",
                },
            };

        private static IEnumerable<UserModel> UserModels => new List<UserModel>()
            {
                new UserModel()
                {
                    Id = 1,
                    UserName = "user1",
                    FirstName = "FirstUser1",
                    LastName= "LastUser1",
                },

                new UserModel()
                {
                    Id = 2,
                    UserName = "user2",
                    FirstName = "FirstUser2",
                    LastName= "LastUser2",
                },

                new UserModel()
                {
                    Id = 3,
                    UserName = "user3",
                    FirstName = "FirstUser3",
                    LastName= "LastUser3",
                    Bids = new List<BidModel>()
                    {
                        new BidModel()
                        {
                            Id = 1,
                            BidValue = 100
                        }
                    }
                },

                new UserModel()
                {
                    Id = 4,
                    UserName = "user4",
                    FirstName = "FirstUser4",
                    LastName= "LastUser4",
                    Bids = new List<BidModel>()
                    {
                        new BidModel()
                        {
                            Id = 2,
                            BidValue = 150
                        }
                    }
                },

                new UserModel()
                {
                    Id = 5,
                    UserName = "user5",
                    FirstName = "FirstUser5",
                    LastName= "LastUser5",
                },

                new UserModel()
                {
                    Id = 6,
                    UserName = "user6",
                    FirstName = "FirstUser6",
                    LastName= "LastUser6",
                },
            };

        public static IEnumerable<object[]> GetById_TestData()
        {
            var users = UserModels.ToList();

            yield return new object[] { 1, users[0] };
            yield return new object[] { 2, users[1] };
            yield return new object[] { 3, users[2] };
            yield return new object[] { -1, null };
            yield return new object[] { 0, null };
            yield return new object[] { 120, null };
        }

        public static IEnumerable<object[]> GetByUserName_TestData()
        {
            var users = UserModels.ToList();
            yield return new object[] { users[0].UserName, users[0] };
            yield return new object[] { users[1].UserName, users[1] };
            yield return new object[] { users[2].UserName, users[2] };
            yield return new object[] { "This user doesn't exist", null };
            yield return new object[] { null, null };
        }

        #endregion

       [Fact]
        public async Task Get_ShouldReturnCorrectData()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.UserRepository.GetAsync())
                          .ReturnsAsync(UserEntities);

            var mapper = UnitTestHelpers.CreateMapper();

            var userService = new UserService(unitOfWorkMock.Object, mapper);

            var expected = UserModels;

            // Act
            var actual = await userService.GetAsync();

            // Assert
            Assert.Equal(expected, actual, new UserModelComparer());
        }

        [Theory]
        [MemberData(nameof(GetById_TestData))]
        public async Task GetById_ShouldReturnCorrectData(int userId, UserModel expected)
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.UserRepository.GetByIdAsync(It.IsAny<int>()))
                          .ReturnsAsync((int id) => UserEntities.FirstOrDefault(u => u.Id == id));

            var mapper = UnitTestHelpers.CreateMapper();

            var userService = new UserService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await userService.GetByIdAsync(userId);

            // Assert
            Assert.Equal(expected, actual, new UserModelComparer());
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.UserRepository.Update(It.IsAny<AppUser>()));
            unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameAsync(It.IsAny<string>()))
                          .ReturnsAsync((string userName) => UserEntities.FirstOrDefault(u => u.UserName == userName));

            var mapper = UnitTestHelpers.CreateMapper();

            var userService = new UserService(unitOfWorkMock.Object, mapper);

            var userName = UserEntities.First().UserName;
            var updateModel = new UserUpdateModel() { FirstName = "NewFirst", LastName = "NewLast", Balance = 901 };

            // Act
            await userService.UpdateAsync(UserEntities.First().UserName, updateModel);

            // Assert
            unitOfWorkMock.Verify(x => x.UserRepository.Update(
                              It.Is<AppUser>(u => u.UserName == userName
                                    && u.FirstName == updateModel.FirstName
                                    && u.LastName == updateModel.LastName 
                                    && u.Balance == updateModel.Balance)), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetByUserName_TestData))]
        public async Task GetByUserName_ShouldReturnCorrectData(string userName, UserModel expected)
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameAsync(It.IsAny<string>()))
                          .ReturnsAsync( (string uName) => UserEntities.FirstOrDefault(u => u.UserName == uName));

            var mapper = UnitTestHelpers.CreateMapper();

            var userService = new UserService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await userService.GetByUserNameAsync(userName);

            // Assert
            Assert.Equal(expected, actual, new UserModelComparer());
        }

        [Fact]
        public async Task GetAllWithDetails_ShouldReturnCorrectData()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.UserRepository.GetAllWithDetailsAsync())
                          .ReturnsAsync(UserEntities);

            var mapper = UnitTestHelpers.CreateMapper();

            var userService = new UserService(unitOfWorkMock.Object, mapper);

            var expected = UserModels;

            // Act
            var actual = await userService.GetAllWithDetailsAsync();

            // Assert
            Assert.Equal(expected, actual, new UserModelComparer());
            Assert.Contains(actual, u => u.Bids?.Count > 0);
        }

        [Theory]
        [MemberData(nameof(GetByUserName_TestData))]
        public async Task GetByUserNameWithDetails_ShouldReturnCorrectData(string userName, UserModel expected)
        {
            // Assert
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameWithDetailsAsync(It.IsAny<string>()))
                          .ReturnsAsync((string uName) => UserEntities.FirstOrDefault(u => u.UserName == uName));

            var mapper = UnitTestHelpers.CreateMapper();

            var userService = new UserService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await userService.GetByUserNameWithDetailsAsync(userName);

            // Assert
            Assert.Equal(expected, actual, new UserModelComparer());
        }
    }
}