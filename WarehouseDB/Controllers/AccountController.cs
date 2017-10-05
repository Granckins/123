using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using WarehouseDB.Filters;
using WarehouseDB.Models;
using LoveSeat;
using WarehouseDB.Services;
using System.Web;
using Warehouse.Core.Repositories;
using System.Threading.Tasks; 
namespace WarehouseDB.Controllers
{
    [Authorize] 
    public class AccountController : Controller
    {

        WarehouseMembership membership = new WarehouseMembership();
 
        
        [HttpPost]

        [AllowAnonymous]
        public bool Login(LogOnModel model)
        {
              if (ModelState.IsValid)
            {
                if (membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    var MyCookie = FormsAuthentication.GetAuthCookie(User.Identity.Name.ToString(),
                                                             false);
                      Response.AppendCookie(MyCookie);
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    return true;
               
                  
            }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return false;
                }
             }
              else
              {
                  ModelState.AddModelError("", "Invalid login attempt.");
                  return false;
              }
        }
       
 
        
        [HttpPost]
        public void RemoteLogin()
        {
            string username = Request["UserName"];
            string password = Request["Password"];
            bool result = membership.ValidateUser(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
        }
        //
        // GET: /Account/LogOff
        [HttpPost]
        
          [AllowAnonymous] 
        public bool LogOff (LogOnModel model)
        {
            FormsAuthentication.SignOut();

            return true;
        }


    }
}