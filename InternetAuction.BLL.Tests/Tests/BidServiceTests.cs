//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using InternetAuction.BLL.Models;
//using InternetAuction.BLL.Models.Bid;
//using InternetAuction.BLL.Services;
//using InternetAuction.BLL.Tests.Comparers;
//using InternetAuction.DAL.Entities;
//using InternetAuction.DAL.Interfaces;
//using InternetAuction.Identity.Entities;
//using Moq;
//using Xunit;

//namespace InternetAuction.BLL.Tests.Tests
//{
//    public class BidServiceTests
//    {
//        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//        private readonly IMapper _mapper;

//        private readonly BiddingService _biddingService;

//        public BidServiceTests()
//        {
//            _unitOfWorkMock = new Mock<IUnitOfWork>();
//            _mapper = UnitTestHelpers.CreateMapper();

//            _biddingService = new BiddingService(_unitOfWorkMock.Object, _mapper);
//        }

//        #region TestData

//        private static IEnumerable<Bid> BidEntities => new List<Bid>()
//        {
//            new Bid()
//            {
//                Id = 1,
//                BidValue = 50,
//                LotId = 1
//            },
            
//            new Bid()
//            {
//                Id = 2,
//                BidValue = 70,
//                LotId = 1
//            },
            
//            new Bid()
//            {
//                Id = 3,
//                BidValue = 90,
//                LotId = 2,
//                Lot = new Lot()
//                {
//                    Id = 2
//                }
//            }
//        };
        
//        private static IEnumerable<BidModel> BidModels => new List<BidModel>()
//        {
//            new BidModel()
//            {
//                Id = 1,
//                BidValue = 50,
//                LotId = 1
//            },
            
//            new BidModel()
//            {
//                Id = 2,
//                BidValue = 70,
//                LotId = 1
//            },
            
//            new BidModel()
//            {
//                Id = 3,
//                BidValue = 90,
//                LotId = 2
//            }
//        };

//        private static IEnumerable<Lot> Lots => new List<Lot>()
//        {
//            new Lot()
//            {
//                Id = 1,
//                SaleEndTime = DateTime.Now.AddMinutes(-30),
//                Seller = new AppUser()
//                {
//                    UserName = "user3"
//                }
//            },
            
//            new Lot()
//            {
//                Id = 2,
//                SaleEndTime = DateTime.Now.AddMinutes(-1),
//                Seller = new AppUser()
//                {
//                    UserName = "user3"
//                }
//            },

//            new Lot()
//            {
//                Id = 3,
//                SaleEndTime = DateTime.Now.AddMinutes(50),
//                InitialPrice = 20,
//                Bids = new List<Bid>()
//                {
//                    new Bid()
//                    {
//                        BidValue = 35,
//                    },

//                    new Bid()
//                    {
//                        BidValue = 62,
//                    }
//                },
//                Seller = new AppUser()
//                {
//                    UserName = "user3"
//                }
//            },
            
//            new Lot()
//            {
//                Id = 4,
//                InitialPrice = 50,
//                SaleEndTime = DateTime.Now.AddMinutes(10),
//                Seller = new AppUser()
//                {
//                    UserName = "user3"
//                }
//            },

//            new Lot()
//            {
//                Id = 5,
//                InitialPrice = 1,
//                SaleEndTime = DateTime.Now.AddHours(3),
//                Seller = new AppUser()
//                {
//                    UserName = "user3"
//                }
//            }
//        };

//        private static IEnumerable<AppUser> Users => new List<AppUser>()
//        {
//            new AppUser()
//            {
//                Id = 1,
//                UserName = "user1"
//            },

//            new AppUser()
//            {
//                Id = 2,
//                UserName = "user2"
//            },

//            new AppUser()
//            {
//                Id = 3,
//                UserName = "user3",
//            }
//        };

//        public static IEnumerable<object[]> GetById_TestData()
//        {
//            var bids = BidModels.ToList();

//            yield return new object[] {1, bids[0]};
//            yield return new object[] {2, bids[1]};
//            yield return new object[] {3, bids[2]};
//            yield return new object[] {0, null};
//            yield return new object[] {100, null};
//            yield return new object[] {-5, null};
//        }

//        #endregion

//        [Fact]
//        public async Task Get_ShouldReturnCorrectData()
//        {
//            // Arrange
//            _unitOfWorkMock.Setup(x => x.BidRepository.GetAsync())
//                           .ReturnsAsync(BidEntities);

//            var expected = BidModels;

//            // Act
//            var actual = await _biddingService.GetAsync();

//            // Assert
//            Assert.Equal(expected, actual, new BidModelComparer());
//        }

//        [Theory]
//        [MemberData(nameof(GetById_TestData))]
//        public async Task GetById_ShouldReturnCorrectBid(int bidId, BidModel expected)
//        {
//            // Arrange
//            _unitOfWorkMock.Setup(x => x.BidRepository.GetByIdAsync(It.IsAny<int>()))
//                           .ReturnsAsync((int id) => BidEntities.FirstOrDefault(b => b.Id == id));

//            // Act
//            var actual = await _biddingService.GetByIdAsync(bidId);

//            // Assert
//            Assert.Equal(expected, actual, new BidModelComparer());
//        }

//        [Theory]
//        [InlineData(100, 1)]
//        [InlineData(101, 2)]
//        public async Task Add_ShouldThrowWithInvalidDate(decimal bidValue, int lotId)
//        {
//            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
//                           .ReturnsAsync((int lotId) => Lots.FirstOrDefault(l => l.Id == lotId));

//            var createModel = new BidCreateModel() { BidValue = bidValue, LotId = lotId, BidderUserName = "somebody" };

//            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _biddingService.AddAsync(createModel));
//        }

//        [Theory]
//        [InlineData(40, 4)]
//        [InlineData(59.99, 4)]
//        [InlineData(71, 3)]
//        public async Task Add_ShouldThrowWithInvalidBidValue(decimal bidValue, int lotId)
//        {
//            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
//                           .ReturnsAsync((int lotId) => Lots.FirstOrDefault(l => l.Id == lotId));

//            var createModel = new BidCreateModel() {BidValue = bidValue, LotId = lotId, BidderUserName = "somebody" };

//            await Assert.ThrowsAsync<ArgumentException>(async () => await _biddingService.AddAsync(createModel));
//        }

//        [Fact]
//        public async Task Add_ShouldThrowWhenBiddingOnYourOwnLot()
//        {
//            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
//                           .ReturnsAsync((int lotId) => Lots.FirstOrDefault(l => l.Id == lotId));

//            var createModel = new BidCreateModel() { BidValue = 999, LotId = 5, BidderUserName = "user3"};

//            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _biddingService.AddAsync(createModel));
//        }

//        [Fact]
//        public async Task Add_ShouldAddWithCorrectData()
//        {
//            // Arrange
//            _unitOfWorkMock.Setup(x => x.BidRepository.Add(It.IsAny<Bid>()));

//            _unitOfWorkMock.Setup(x => x.LotRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
//                           .ReturnsAsync((int lotId) => Lots.FirstOrDefault(l => l.Id == lotId));

//            _unitOfWorkMock.Setup(x => x.UserRepository.GetByUserNameAsync(It.IsAny<string>()))
//                           .ReturnsAsync((string userName) => Users.FirstOrDefault(u => u.UserName == userName));

//            var createModel = new BidCreateModel() {BidValue = 200, LotId = 3, BidderUserName = "user1"};

//            // Act
//            await _biddingService.AddAsync(createModel);

//            // Assert
//            _unitOfWorkMock.Verify(x => x.BidRepository.Add(It.Is<Bid>(b => b.BidValue == createModel.BidValue
//                                                                                  && b.Bidder.UserName == createModel.BidderUserName)), Times.Once);
//            _unitOfWorkMock.Verify(x => x.SaveChangesAsync());
//        }

//        [Fact]
//        public async Task DeleteById_ShouldDelete()
//        {
//            // Arrange
//            _unitOfWorkMock.Setup(x => x.BidRepository.DeleteByIdAsync(It.IsAny<int>()));

//            var bidId = 1;

//            // Act
//            await _biddingService.DeleteByIdAsync(bidId); 

//            // Assert
//            _unitOfWorkMock.Verify(x => x.BidRepository.DeleteByIdAsync(It.Is<int>(id => id == bidId)), Times.Once);
//            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
//        }
//    }
//}