using System.Collections.Generic;
using InternetAuction.DAL.Interfaces;

namespace InternetAuction.DAL.Entities
{
    public class LotCategory : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Lot> Lots { get; set; }
    }
}