using System.Threading.Tasks;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public ILotCategoryRepository LotCategoryRepository { get; }

        public ILotRepository LotRepository { get; }

        public IBidRepository BidRepository { get; }

        public IImageRepository ImageRepository { get; }

        private readonly AuctionContext _context;

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