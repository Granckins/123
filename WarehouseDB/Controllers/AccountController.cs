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
namespace WarehouseDB.Controllers
{
    public class AccountController : Controller
    {

        PrimaMembership membership = new PrimaMembership();

        //[HttpGet]
        //public ActionResult LogOn() {
        //    return View();
        //}
        [HttpGet]
        public ActionResult LogOn(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                CLogger.WriteLog(ELogLevel.DEBUG, "null key");
                return View();
            }
            try
            {
                CLogger.WriteLog(ELogLevel.DEBUG, "key: " + key);
                var phrase = PasswordHash.Decrypt(key);
                CLogger.WriteLog(ELogLevel.DEBUG, "phrase: " + phrase);
                var phrases = phrase.Split("@".ToCharArray()).Select(x => HttpUtility.UrlDecode(x)).ToList();
                var userName = phrases[0];
                var password = phrases[1];
                var date = DateTime.Parse(phrases[2]);
                CLogger.WriteLog(ELogLevel.DEBUG, "phrase" + phrase);
                if (date.AddSeconds(45) > DateTime.Now && membership.ValidateUser(userName, password))
                {
                    FormsAuthentication.SetAuthCookie(userName, false);
                    //var cookie = FormsAuthentication.GetAuthCookie(User.Identity.Name.ToString(), false);
                    //cookie.Domain = "v2.prima-inform.ru";//the second level domain name
                    //Response.AppendCookie(cookie);

                    return RedirectToAction("Index", "Home");

                }
                else throw new Exception("asdfasdf");

            }
            catch (Exception exc)
            {
                CLogger.WriteLog(ELogLevel.DEBUG, exc.ToString());
                return View("LogOn");
            }
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    //var MyCookie = FormsAuthentication.GetAuthCookie(User.Identity.Name.ToString(),
                    //                                         false);
                    //MyCookie.Domain = "v2.prima-inform.ru";//the second level domain name
                    //Response.AppendCookie(MyCookie);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Prima");
                        //return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    var rep = new MongoRequestsRepository();
                    var us = rep.GetUserIdentityByName(model.UserName);
                    ModelState.AddModelError("",
                        us.UserId == "000000000000000000000000" ? "Доступ закрыт." : "Неверные логин и пароль.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            var logoffUrl = ConfigurationManager.AppSettings["logoff-url"] ?? "http://www.prima-inform.ru";

            return Redirect(logoffUrl);
        }

    }
}