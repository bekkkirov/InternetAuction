using System.Threading.Tasks;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Repositories
{
    ///<inheritdoc cref="IUnitOfWork"/>
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public ILotCategoryRepository LotCategoryRepository { get; }

        public ILotRepository LotRepository { get; }

        public IBidRepository BidRepository { get; }

        public IImageRepository ImageRepository { get; }

        /// <summary>
        /// Database context.
        /// </summary>
        private readonly AuctionContext _context;

        /// <summary>
        /// Creates an instance of the unit of work.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="userRepository">User repository.</param>
        /// <param name="lotCategoryRepository">Lot category repository.</param>
        /// <param name="lotRepository">Lot repository.</param>
        /// <param name="bidRepository">Bid repository.</param>
        /// <param name="imageRepository">Image repository.</param>
        public UnitOfWork(AuctionContext context, IUserRepository userRepository,
                          ILotCategoryRepository lotCategoryRepository, ILotRepository lotRepository,
                          IBidRepository bidRepository, IImageRepository imageRepository)
        {
            _context = context;
            UserRepository = userRepository;
            LotCategoryRepository = lotCategoryRepository;
            LotRepository = lotRepository;
            BidRepository = bidRepository;
            ImageRepository = imageRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}