using AutoMapper;
using InternetAuction.BLL.Models.Lot;
using InternetAuction.BLL.Pagination;
using InternetAuction.BLL.Services;
using InternetAuction.BLL.Tests.Comparers;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InternetAuction.BLL.Tests.Tests
{
    public class LotServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        private readonly LotService _lotService;

        public LotServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapper = UnitTestHelpers.CreateMapper();

            _lotService = new LotService(_unitOfWorkMock.Object, _mapper);
        }

        #region TestData

        private static IEnumerable<Lot> LotEntities => new List<Lot>()
        {
            new Lot()
            {
                Id = 1,
                Name = "Lot1",
                Description = "Description",
                InitialPrice = 120,
                CategoryId = 1,
                SaleEndTime = DateTime.Now.AddMinutes(30),
                Category = new LotCategory() {Name = "Category1"}
            },

            new Lot()
            {
                Id = 2,
                Name = "Lot1",
                Description = "Description",
                InitialPrice = 12,
                CategoryId = 2,
                SaleEndTime = DateTime.Now.AddMinutes(30),
                Category = new LotCategory() {Name = "Category1"}
            },

            new Lot()
            {
                Id = 3,
                Name = "Lot1",
                Description = "Description",
                InitialPrice = 68,
                CategoryId = 1,
                Category = new LotCategory() {Name = "Category1"},
                SaleEndTime = DateTime.Now.AddMinutes(30),
                Seller = new AppUser() {UserName = "user3"}
            },

            new Lot()
            {
                Id = 4,
                Name = "Lot1",
                Description = "Description",
                InitialPrice = 14,
                CategoryId = 2,
                SaleEndTime = DateTime.Now.AddMinutes(30),
                Category = new LotCategory() {Name = "Category1"}
            },

            new Lot()
            {
                Id = 5,
                Name = "Lot1",
                Description = "Description",
                InitialPrice = 99,
                CategoryId = 2,
                SaleEndTime = DateTime.Now.AddMinutes(-30),
                Category = new LotCategory() {Name = "Category1"},
                Seller = new AppUser() {UserName = "user3"}
            }
        };

        private static IEnumerable<LotModel> LotModels => new List<LotModel>()
        {
            new LotModel()
            {
                Id = 1,
                Name = "Lot1",
                Description = "Description",
                CurrentPrice = 120
            },

            new LotModel()
            {
                Id = 2,
                Name = "Lot1",
                Description = "Description",
                CurrentPrice = 12
            },

            new LotModel()
            {
                Id = 3,
                Name = "Lot1",
                Description = "Description",
                CurrentPrice = 68
            },

            new LotModel()
            {
                Id = 4,
                Name = "Lot1",
                Description = "Description",
                CurrentPrice = 14
            },

            new LotModel()
            {
                Id = 5,
                Name = "Lot1",
                Description = "Description",
                CurrentPrice = 99,
            }
        };

        private static IEnumerable<LotPreviewModel> LotPreviewModels => new List<LotPreviewModel>()
        {
            new LotPreviewModel()
            {
                Id = 1,
                Name = "Lot1",
                CurrentPrice = 120
            },

            new LotPreviewModel()
            {
                Id = 2,
                Name = "Lot1",
                CurrentPrice = 12
            },

            new LotPreviewModel()
            {
                Id = 3,
                Name = "Lot1",
                CurrentPrice = 68
            },

            new LotPreviewModel()
            {
                Id = 4,
                Name = "Lot1",
                CurrentPrice = 14
            },
        };

        private static IEnumerable<LotCategory> LotCategories => new List<LotCategory>()
        {
            new LotCategory()
            {
                Id = 1,
                Name = "Category1"
            },

            new LotCategory()
            {
                Id = 2,
                Name = "Category2"
            },

            new LotCategory()
            {
                Id = 3,
                Name = "Category3"
            },
        };

        private static IEnumerable<LotCategoryModel> LotCategoryModels => new List<LotCategoryModel>()
        {
            new LotCategoryModel()
            {
                Id = 1,
                Name = "Category1"
            },

            new LotCategoryModel()
            {
                Id = 2,
                Name = "Category2"
            },

            new LotCategoryModel()
            {
                Id = 3,
                Name = "Category3"
            },
        };

        private static IEnumerable<AppUser> UserEntities => new List<AppUser>()
        {
            new AppUser()
            {
                Id = 1,
                UserName = "user1"
            },

            new AppUser()
            {
                Id = 2,
                UserName = "user2"
            },

            new AppUser()
            {
                Id = 3,
                UserName = "user3"
            },
        };

        public static IEnumerable<object[]> GetLotById_TestData()
        {
            var lots = LotModels.ToList();

            yield return new object[] {1, lots[0]};
            yield return new object[] {2, lots[1]};
            yield return new object[] {3, lots[2]};
            yield return new object[] {-10, null};
            yield return new object[] {158, null};
        }

        public static IEnumerable<object[]> GetLotsPreviewsByCategory_TestData()
        {
            int count = 2;
            var categoriesIds = new List<int>() {1, 2};

            var lotParameters = new LotParameters();
            var mapper = UnitTestHelpers.CreateMapper();

            List<PagedList<LotPreviewModel>> lists = new List<PagedList<LotPreviewModel>>(count);

            for (int i = 0; i < count; i++)
            {
                var lots = mapper.Map<IEnumerable<LotPreviewModel>>(LotEntities.Where(l =>
                    l.CategoryId == categoriesIds[i]));
                lists.Add(
                    PagedList<LotPreviewModel>.CreateAsync(lots, lotParameters.PageNumber, lotParameters.PageSize));
            }

            yield return new object[] {categoriesIds[0], lists[0]};
            yield return new object[] {categoriesIds[1], lists[1]};
        }

        #endregion

        [Fact]
        public async Task GetLots_ShouldReturnCorrectData()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotRepository.GetAsync())
                           .ReturnsAsync(LotEntities);

            var expected = LotModels;

            // Act
            var actual = await _lotService.GetAsync();

            // Assert
            Assert.Equal(expected, actual, new LotModelComparer());
        }

        [Theory]
        [MemberData(nameof(GetLotById_TestData))]
        public async Task GetLotById_ShouldReturnCorrectData(int lotId, LotModel expected)
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                           .ReturnsAsync((int id) => LotEntities.FirstOrDefault(l => l.Id == id));

            // Act
            var actual = await _lotService.GetByIdWithDetailsAsync(lotId);

            // Assert
            Assert.Equal(expected, actual, new LotModelComparer());
        }

        [Fact]
        public async Task GetPreviews_ShouldReturnCorrectData()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotRepository.GetPreviewsAsync())
                           .ReturnsAsync(LotEntities.Where(l => l.SaleEndTime >= DateTime.Now));

            var lotParameters = new LotParameters();
            var expected =
                PagedList<LotPreviewModel>.CreateAsync(LotPreviewModels, lotParameters.PageNumber,
                    lotParameters.PageSize);

            // Act
            var actual = await _lotService.GetLotsPreviewsAsync(lotParameters);

            // Assert
            Assert.Equal(expected, actual, new LotPreviewModelComparer());
        }

        [Theory]
        [MemberData(nameof(GetLotsPreviewsByCategory_TestData))]
        public async Task GetLotsPreviewsByCategory_ShouldReturnCorrectData(int categoryId,
            PagedList<LotPreviewModel> expected)
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotRepository.GetPreviewsByCategoryIdAsync(It.IsAny<int>()))
                           .ReturnsAsync((int id) => LotEntities.Where(l => l.CategoryId == id));

            var lotParameters = new LotParameters();

            // Act
            var actual = await _lotService.GetLotsPreviewsByCategoryAsync(categoryId, lotParameters);

            // Assert
            Assert.Equal(expected, actual, new LotPreviewModelComparer());
        }

        [Fact]
        public async Task AddLot_ShouldAddLot()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotRepository.Add(It.IsAny<Lot>()));
            _unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameAsync(It.IsAny<string>()))
                           .ReturnsAsync((string userName) => UserEntities.FirstOrDefault(u => u.UserName == userName));

            var lot = new LotCreateModel() {Name = "Lot"};
            var sellerUserName = "user1";

            // Act
            await _lotService.AddAsync(lot, sellerUserName);

            // Assert
            _unitOfWorkMock.Verify(
                x => x.LotRepository.Add(It.Is<Lot>(l => l.Name == lot.Name && l.Seller.UserName == sellerUserName)),
                Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteLotById_ShouldThrowWithInvalidSellerName()
        {
            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                           .ReturnsAsync((int id) => LotEntities.FirstOrDefault(l => l.Id == id));

            var lotId = 3;
            var userName = "SomeInvalidUserName";

            await Assert.ThrowsAsync<ArgumentException>(() => _lotService.DeleteByIdAsync(userName, lotId));
        }

        [Fact]
        public async Task DeleteLotById_ShouldThrowWhenSaleEnded()
        {
            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                           .ReturnsAsync((int id) => LotEntities.FirstOrDefault(l => l.Id == id));

            var lotId = 5;
            var userName = "user3";

            await Assert.ThrowsAsync<ArgumentException>(() => _lotService.DeleteByIdAsync(userName, lotId));
        }

        [Fact]
        public async Task DeleteLotById_ShouldDeleteLot()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotRepository.DeleteByIdAsync(It.IsAny<int>()));
            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                           .ReturnsAsync((int id) => LotEntities.FirstOrDefault(l => l.Id == id));

            var lotId = 3;
            var userName = "user3";

            // Act
            await _lotService.DeleteByIdAsync(userName, lotId);

            // Assert
            _unitOfWorkMock.Verify(x => x.LotRepository.DeleteByIdAsync(It.Is<int>(id => id == lotId)), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnCorrectData()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotCategoryRepository.GetAsync())
                           .ReturnsAsync(LotCategories);

            var expected = LotCategoryModels;

            // Act
            var actual = await _lotService.GetAllCategoriesAsync();

            // Assert
            Assert.Equal(expected, actual, new LotCategoryModelComparer());
        }

        [Fact]
        public async Task AddCategory_ShouldAddCategory()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotCategoryRepository.Add(It.IsAny<LotCategory>()));

            var category = new LotCategoryCreateModel() {Name = "Category"};

            // Act
            await _lotService.AddCategoryAsync(category);

            // Assert
            _unitOfWorkMock.Verify(x => x.LotCategoryRepository.Add(It.Is<LotCategory>(c => c.Name == category.Name)),
                Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryById_ShouldDeleteCategory()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.LotCategoryRepository.DeleteByIdAsync(It.IsAny<int>()));

            var categoryToDelete = new LotCategory() {Id = 1, Name = "Category"};

            // Act
            await _lotService.DeleteCategoryByIdAsync(categoryToDelete.Id);

            // Assert
            _unitOfWorkMock.Verify(
                x => x.LotCategoryRepository.DeleteByIdAsync(It.Is<int>(id => id == categoryToDelete.Id)), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData("o")]
        [InlineData("lot")]
        [InlineData("pla")]
        public async Task Search_ShouldReturnCorrectData(string searchValue)
        {
            _unitOfWorkMock.Setup(x => x.LotRepository.SearchAsync(It.IsAny<string>()))
                           .ReturnsAsync((string search) => LotEntities.Where(l =>
                               EF.Functions.Like(l.Name, $"%{search}%")
                               && l.SaleEndTime >= DateTime.Now));

            var lotParameters = new LotParameters();
            var expected = PagedList<LotPreviewModel>.CreateAsync(LotPreviewModels.Where(l =>
                    EF.Functions.Like(l.Name, $"%{searchValue}%")),
                lotParameters.PageNumber, lotParameters.PageSize);

            var actual = await _lotService.SearchAsync(searchValue, lotParameters);

            Assert.Equal(expected, actual, new LotPreviewModelComparer());
        }
    }
}