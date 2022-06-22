﻿using System.Collections.Generic;

namespace InternetAuction.BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Balance { get; set; }

        public ImageModel ProfileImage { get; set; }

        public List<LotModel> RegisteredLots { get; set; }

        public List<LotModel> BoughtLots { get; set; }

        public List<BidModel> Bids { get; set; }
    }
}