using InternetAuction.BLL.Models;
using InternetAuction.BLL.Pagination;
using InternetAuction.BLL.Services;
using InternetAuction.BLL.Tests.Comparers;
using InternetAuction.DAL.Entities;
using InternetAuction.DAL.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InternetAuction.BLL.Tests
{
    public class LotServiceTests
    {
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
                    Category = new LotCategory() {Name = "Category1"}
                },

                new Lot()
                {
                    Id = 2,
                    Name = "Lot1",
                    Description = "Description",
                    InitialPrice = 12,
                    CategoryId = 2,
                    Category = new LotCategory() {Name = "Category1"}
                },

                new Lot()
                {
                    Id = 3,
                    Name = "Lot1",
                    Description = "Description",
                    InitialPrice = 68,
                    CategoryId = 1,
                    Category = new LotCategory() {Name = "Category1"}
                },

                new Lot()
                {
                    Id = 4,
                    Name = "Lot1",
                    Description = "Description",
                    InitialPrice = 14,
                    CategoryId = 2,
                    Category = new LotCategory() {Name = "Category1"}
                },
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

        public static IEnumerable<object[]> GetLotById_TestData()
        {
            var lots = LotModels.ToList();

            yield return new object[] { 1, lots[0] };
            yield return new object[] { 2, lots[1] };
            yield return new object[] { 3, lots[2] };
            yield return new object[] { -10, null };
            yield return new object[] { 158, null };
        }

        public static IEnumerable<object[]> GetLotsPreviewsByCategory_TestData()
        {
            int count = 2;
            var categoriesIds = new List<int>() { 1, 2 };

            var lotParameters = new LotParameters();
            var mapper = UnitTestHelpers.CreateMapper();

            List<PagedList<LotPreviewModel>> lists = new List<PagedList<LotPreviewModel>>(count);

            for (int i = 0; i < count; i++)
            {
                var lots = mapper.Map<IEnumerable<LotPreviewModel>>(LotEntities.Where(l => l.CategoryId == categoriesIds[i]));
                lists.Add(PagedList<LotPreviewModel>.CreateAsync(lots, lotParameters.PageNumber, lotParameters.PageSize));
            }

            yield return new object[] { categoriesIds[0], lists[0] };
            yield return new object[] { categoriesIds[1], lists[1] };
        }

        #endregion

        [Fact]
        public async Task GetLots_ShouldReturnCorrectData()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotRepository.GetAsync())
                          .ReturnsAsync(LotEntities);

            var mapper = UnitTestHelpers.CreateMapper();
            var expected = LotModels;

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await lotService.GetAsync();

            // Assert
            Assert.Equal(expected, actual, new LotModelComparer());
        }

        [Theory]
        [MemberData(nameof(GetLotById_TestData))]
        public async Task GetLotById_ShouldReturnCorrectData(int lotId, LotModel expected)
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                          .ReturnsAsync((int id) => LotEntities.FirstOrDefault(l => l.Id == id));

            var mapper = UnitTestHelpers.CreateMapper();

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await lotService.GetByIdWithDetailsAsync(lotId);

            // Assert
            Assert.Equal(expected, actual, new LotModelComparer());
        }

        [Fact]
        public async Task GetPreviews_ShouldReturnCorrectData()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotRepository.GetPreviewsAsync())
                          .ReturnsAsync(LotEntities);

            var mapper = UnitTestHelpers.CreateMapper();

            var lotParameters = new LotParameters();
            var expected = PagedList<LotPreviewModel>.CreateAsync(LotPreviewModels, lotParameters.PageNumber, lotParameters.PageSize);

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await lotService.GetLotsPreviewsAsync(lotParameters);

            // Assert
            Assert.Equal(expected, actual, new LotPreviewModelComparer());
        }

        [Theory]
        [MemberData(nameof(GetLotsPreviewsByCategory_TestData))]
        public async Task GetLotsPreviewsByCategory_ShouldReturnCorrectData(int categoryId, PagedList<LotPreviewModel> expected)
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotRepository.GetPreviewsByCategoryIdAsync(It.IsAny<int>()))
                          .ReturnsAsync((int id) => LotEntities.Where(l => l.CategoryId == id));

            var mapper = UnitTestHelpers.CreateMapper();
            var lotParameters = new LotParameters();

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await lotService.GetLotsPreviewsByCategoryAsync(categoryId, lotParameters);

            // Assert
            Assert.Equal(expected, actual, new LotPreviewModelComparer());
        }

        [Fact]
        public async Task AddLot_ShouldAddLot()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotRepository.Add(It.IsAny<Lot>()));

            var mapper = UnitTestHelpers.CreateMapper();

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            var lot = new LotCreateModel() { Name = "Lot" };
            var sellerId = 1;

            // Act
            await lotService.AddAsync(lot, sellerId);

            // Assert
            unitOfWorkMock.Verify(x => x.LotRepository.Add(It.Is<Lot>(l => l.Name == lot.Name && l.SellerId == sellerId)), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteLotById_ShouldDeleteLot()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotRepository.DeleteByIdAsync(It.IsAny<int>()));

            var mapper = UnitTestHelpers.CreateMapper();

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            var lotToDelete = new Lot() { Id = 1, Name = "Lot" };

            // Act
            await lotService.DeleteByIdAsync(lotToDelete.Id);

            // Assert
            unitOfWorkMock.Verify(x => x.LotRepository.DeleteByIdAsync(It.Is<int>(id => id == lotToDelete.Id)), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnCorrectData()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotCategoryRepository.GetAsync()).ReturnsAsync(LotCategories);

            var mapper = UnitTestHelpers.CreateMapper();
            var expected = LotCategoryModels;

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            // Act
            var actual = await lotService.GetAllCategoriesAsync();

            // Assert
            Assert.Equal(expected, actual, new LotCategoryModelComparer());
        }

        [Fact]
        public async Task AddCategory_ShouldAddCategory()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotCategoryRepository.Add(It.IsAny<LotCategory>()));

            var mapper = UnitTestHelpers.CreateMapper();

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            var category = new LotCategoryCreateModel() { Name = "Category" };

            // Act
            await lotService.AddCategoryAsync(category);

            // Assert
            unitOfWorkMock.Verify(x => x.LotCategoryRepository.Add(It.Is<LotCategory>(c => c.Name == category.Name)), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        } 
        
        [Fact]
        public async Task DeleteCategoryById_ShouldDeleteCategory()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.LotCategoryRepository.DeleteByIdAsync(It.IsAny<int>()));

            var mapper = UnitTestHelpers.CreateMapper();

            var lotService = new LotService(unitOfWorkMock.Object, mapper);

            var categoryToDelete = new LotCategory() { Id = 1, Name = "Category" };

            // Act
            await lotService.DeleteCategoryByIdAsync(categoryToDelete.Id);

            // Assert
            unitOfWorkMock.Verify(x => x.LotCategoryRepository.DeleteByIdAsync(It.Is<int>(id => id == categoryToDelete.Id)), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}