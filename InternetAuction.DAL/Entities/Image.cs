using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    public class Image : IEntity
    {
        public int Id { get; set; }

        public string PublicId { get; set; }

        public string Url { get; set; }

        public int? UserId { get; set; }
        public AppUser User { get; set; }

        public int? LotId { get; set; }
        public Lot Lot { get; set; }
    }
}