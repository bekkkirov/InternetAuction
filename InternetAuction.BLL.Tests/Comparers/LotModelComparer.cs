using System;
using System.Collections.Generic;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Tests.Comparers
{
    public class LotModelComparer : IEqualityComparer<LotModel>
    {
        public bool Equals(LotModel x, LotModel y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(LotModel obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}