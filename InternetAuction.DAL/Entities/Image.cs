using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    /// <summary>
    /// Represents an image.
    /// </summary>
    public class Image : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the public id for this image.
        /// </summary>
        public string PublicId { get; set; }

        /// <summary>
        /// Gets or sets the url for this image.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the user id to which this image refers.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user to which this image refers.
        /// </summary>
        public AppUser User { get; set; }

        /// <summary>
        /// Gets or sets the lot id to which this image refers.
        /// </summary>
        public int? LotId { get; set; }

        /// <summary>
        /// Gets or sets the lot to which this image refers.
        /// </summary>
        public Lot Lot { get; set; }
    }
}