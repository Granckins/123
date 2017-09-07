using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class WarehouseEntities
    {
        //  public static Mongo<User> Users => new Mongo<User>(ConnectionString);
        public static User GetUser(string userId)
        {
         //   return Users.GetById(userId);
            return new User();
        }
    }
}
