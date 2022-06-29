using System.Collections.Generic;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    /// <summary>
    /// Represents a lot category.
    /// </summary>
    public class LotCategory : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of lots that belong to this category.
        /// </summary>
        public List<Lot> Lots { get; set; } = new List<Lot>();
    }
}