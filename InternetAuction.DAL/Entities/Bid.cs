using System;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    /// <summary>
    /// Represents a bid.
    /// </summary>
    public class Bid : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the bid value for this user.
        /// </summary>
        public decimal BidValue { get; set; }

        /// <summary>
        /// Gets or sets the time when this bid was made.
        /// </summary>
        public DateTime BidTime { get; set; }

        /// <summary>
        /// Gets or sets the lot id to which this bid refers.
        /// </summary>
        public int LotId { get; set; }

        /// <summary>
        /// Gets or sets the lot to which this bid refers.
        /// </summary>
        public Lot Lot { get; set; }

        /// <summary>
        /// Gets or sets the id of user who made this bid.
        /// </summary>
        public int BidderId { get; set; }

        /// <summary>
        /// Gets or sets the user who made this bid.
        /// </summary>
        public AppUser Bidder { get; set; }
    }
}