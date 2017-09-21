using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;

namespace WarehouseDB.Controllers
{
    public class WorkController : Controller
    {
       
        WarehouseRequestsRepository Repository = new WarehouseRequestsRepository();


          [HttpGet]
        public JsonResult GetDocuments(int page = 1, int limit = 10)
        {
          var res=  Repository.GetDocuments();
          return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}
