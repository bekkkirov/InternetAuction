using System;
using System.Collections.Generic;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Tests.Comparers
{
    public class BidModelComparer : IEqualityComparer<BidModel>
    {
        public bool Equals(BidModel x, BidModel y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Id == y.Id && x.BidValue == y.BidValue;
        }

        public int GetHashCode(BidModel obj)
        {
            return HashCode.Combine(obj.Id, obj.BidValue);
        }
    }
}