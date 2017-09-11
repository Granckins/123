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

        //[HttpGet]
        //public ActionResult LogOn() {
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult LogOn(string key)
        //{
        //    if (string.IsNullOrWhiteSpace(key))
        //    {
        //    //    CLogger.WriteLog(ELogLevel.DEBUG, "null key");
        //        return View();
        //    }
        //    try
        //    {
        //      //  CLogger.WriteLog(ELogLevel.DEBUG, "key: " + key);
        //        var phrase = PasswordHash.Decrypt(key);
        //      //  CLogger.WriteLog(ELogLevel.DEBUG, "phrase: " + phrase);
        //        var phrases = phrase.Split("@".ToCharArray()).Select(x => HttpUtility.UrlDecode(x)).ToList();
        //        var userName = phrases[0];
        //        var password = phrases[1];
        //        var date = DateTime.Parse(phrases[2]);
        //     //   CLogger.WriteLog(ELogLevel.DEBUG, "phrase" + phrase);
        //        if (date.AddSeconds(45) > DateTime.Now && membership.ValidateUser(userName, password))
        //        {
        //            FormsAuthentication.SetAuthCookie(userName, false);
        //            //var cookie = FormsAuthentication.GetAuthCookie(User.Identity.Name.ToString(), false);
        //            //cookie.Domain = "v2.prima-inform.ru";//the second level domain name
        //            //Response.AppendCookie(cookie);

        //            return RedirectToAction("Index", "Home");

        //        }
        //        else throw new Exception("asdfasdf");

        //    }
        //    catch (Exception exc)
        //    {
        //       // CLogger.WriteLog(ELogLevel.DEBUG, exc.ToString());
        //        return View("LogOn");
        //    }
        //}

        
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
        //public ActionResult LogOn(LogOnModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (membership.ValidateUser(model.UserName, model.Password))
        //        {
        //            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //            var MyCookie = FormsAuthentication.GetAuthCookie(User.Identity.Name.ToString(),
        //                                                     false);
        //            MyCookie.Domain = "v2.prima-inform.ru";//the second level domain name
        //            Response.AppendCookie(MyCookie);
        //            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //            {
        //                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //                   return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
                  
        //            ModelState.AddModelError("",  "Неверные логин и пароль.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return RedirectToAction("Index", "Home",model);
        //}
         
 
        
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