using System.Threading.Tasks;

namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represents a unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// User repository.
        /// </summary>
        public IUserRepository UserRepository { get; }

        /// <summary>
        /// Lot category repository.
        /// </summary>
        public ILotCategoryRepository LotCategoryRepository { get; }

        /// <summary>
        /// Lot repository.
        /// </summary>
        public ILotRepository LotRepository { get; }

        /// <summary>
        /// Bid repository.
        /// </summary>
        public IBidRepository BidRepository { get; }

        /// <summary>
        /// Image repository.
        /// </summary>
        public IImageRepository ImageRepository { get; }

        /// <summary>
        /// Saves all changes made by this unit of work.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}