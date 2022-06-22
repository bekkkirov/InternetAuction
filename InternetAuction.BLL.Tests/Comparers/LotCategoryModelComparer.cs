using System;
using System.Collections.Generic;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Lot;

namespace InternetAuction.BLL.Tests.Comparers
{
    public class LotCategoryModelComparer : IEqualityComparer<LotCategoryModel>
    {
        public bool Equals(LotCategoryModel x, LotCategoryModel y)
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

        public int GetHashCode(LotCategoryModel obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}