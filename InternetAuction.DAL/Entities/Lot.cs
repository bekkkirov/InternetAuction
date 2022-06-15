using System;
using System.Collections.Generic;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    /// <summary>
    /// Represents a lot.
    /// </summary>
    public class Lot : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this lot.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for this lot.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the initial price this lot.
        /// </summary>
        public decimal InitialPrice { get; set; }

        /// <summary>
        /// Gets or sets the start time for the sale of this lot.
        /// </summary>
        public DateTime SaleStartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the end time for the sale of this lot.
        /// </summary>
        public DateTime SaleEndTime { get; set; }

        /// <summary>
        /// Gets or sets the status for this lot.
        /// </summary>
        public LotStatus Status { get; set; } = LotStatus.Active;

        /// <summary>
        /// Gets or sets the quantity for this lot.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the category id to which this lot refers.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category to which this lot refers.
        /// </summary>
        public LotCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the list of images for this lot.
        /// </summary>
        public List<Image> Images { get; set; } = new List<Image>();

        /// <summary>
        /// Gets or sets the list of bids for this lot.
        /// </summary>
        public List<Bid> Bids { get; set; } = new List<Bid>();

        /// <summary>
        /// Gets or sets the id of user who sells this lot.
        /// </summary>
        public int SellerId { get; set; }

        /// <summary>
        /// Gets or sets the user who sells this lot.
        /// </summary>
        public AppUser Seller { get; set; }

        /// <summary>
        /// Gets or sets the id of user who bought this lot.
        /// </summary>
        public int? BuyerId { get; set; }

        /// <summary>
        /// Gets or sets the user who bought this lot.
        /// </summary>
        public AppUser Buyer { get; set; }
    }
}