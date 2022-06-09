using System.Collections.Generic;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Balance { get; set; }

        public List<Lot> BoughtLots { get; set; }

        public List<Lot> RegisteredLots { get; set; }

        public List<Bid> Bids { get; set; }

        public int? ProfileImageId { get; set; }

        public Image ProfileImage { get; set; }
    }
}