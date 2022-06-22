using System;
using System.Collections.Generic;
using InternetAuction.BLL.Models;
using InternetAuction.BLL.Models.Lot;

namespace InternetAuction.BLL.Tests.Comparers
{
    public class LotPreviewModelComparer : IEqualityComparer<LotPreviewModel>
    {
        public bool Equals(LotPreviewModel x, LotPreviewModel y)
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

        public int GetHashCode(LotPreviewModel obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}