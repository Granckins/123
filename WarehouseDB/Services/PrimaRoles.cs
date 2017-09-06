using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Warehouse.Core;
using Warehouse.Core.Repositories;
namespace WebUI.Services
{
    public class PrimaRoles : RoleProvider
    {
        WarehouseRequestsRepository rep = new WarehouseRequestsRepository();

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();

        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            return rep.GetUserRoles(rep.GetUserIdentityByName(username)).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = rep.GetUserRoles(rep.GetUserIdentityByName(username));
            return roles.Any(x => x.ToLower() == roleName.ToLower());
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}