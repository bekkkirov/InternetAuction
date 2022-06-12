using System.Collections.Generic;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class AppUser : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name for this user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name for this user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the balance for this user.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the list of lots bought by this user.
        /// </summary>
        public List<Lot> BoughtLots { get; set; }

        /// <summary>
        /// Gets or sets the list of lots registered for sale by this user.
        /// </summary>
        public List<Lot> RegisteredLots { get; set; }

        /// <summary>
        /// Gets or sets the list of bids made by this user.
        /// </summary>
        public List<Bid> Bids { get; set; }

        /// <summary>
        /// Gets or sets the profile image id for this user.
        /// </summary>
        public int? ProfileImageId { get; set; }

        /// <summary>
        /// Gets or sets the profile image this user.
        /// </summary>
        public Image ProfileImage { get; set; }
    }
}