using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public ILotCategoryRepository LotCategoryRepository { get; }

        public ILotRepository LotRepository { get; }

        public IBidRepository BidRepository { get; }

        public IImageRepository ImageRepository { get; }

        Task SaveChangesAsync();
    }
}