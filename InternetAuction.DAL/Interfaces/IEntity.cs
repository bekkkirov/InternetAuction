namespace InternetAuction.DAL.Interfaces
{
    /// <summary>
    /// Represents the base entity of the application.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the primary key for this entity.
        /// </summary>
        public int Id { get; set; }
    }
}