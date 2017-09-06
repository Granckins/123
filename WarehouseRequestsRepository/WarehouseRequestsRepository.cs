using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Core;
using Warehouse.Model;

namespace Warehouse.Core.Repositories
{
    public class WarehouseRequestsRepository : IRepositoryRequests
    {
        public UserIdentity GetUserIdentityByName(string username)
        {


            var user = PrimaEntities.Users.Find(x => x.UserName.ToLower() == username.ToLower()
                                                     && !x.IsBlocked && !x.Archive);

            if (user == null)
            {
                return new UserIdentity { UserId = ObjectId.Empty.ToString() };
            }

            return new UserIdentity
            {
                UserId = user.Id,
                Name = user.UserName,
                Password = user.Password,
                Balance = user.Balance,
                IsBlocked = user.IsBlocked
            };
        }
    }
}
