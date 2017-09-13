using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;
using Warehouse.Model;
using WarehouseDB.Services;

namespace WarehouseDB.Controllers
{
    public class HomeController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);


            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                WarehouseRequestsRepository UiRepos = new WarehouseRequestsRepository();
                CurrentUser = UiRepos.GetUserIdentityByName(requestContext.HttpContext.User.Identity.Name);
            }
        }

        private UserIdentity CurrentUser { get; set; }
        public ActionResult Index(string returnUrl)
        {
            ViewData["user"] = CurrentUser;
            // Check notifiers
         //   ViewData["notifies"] = UiRepos.NotifyRepository.GetUnwatchedNotifiesForUser(CurrentUser.Name).ToList();
      
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

    }
}