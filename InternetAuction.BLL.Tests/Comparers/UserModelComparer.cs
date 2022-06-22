using System;
using System.Collections.Generic;
using InternetAuction.BLL.Models;

namespace InternetAuction.BLL.Tests.Comparers
{
    public class UserModelComparer : IEqualityComparer<UserModel>
    {
        public bool Equals(UserModel x, UserModel y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Id == y.Id && x.UserName == y.UserName;
        }

        public int GetHashCode(UserModel obj)
        {
            return HashCode.Combine(obj.Id, obj.UserName);
        }
    }
}