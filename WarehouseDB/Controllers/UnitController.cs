using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;
using Warehouse.Model.Db;

namespace WarehouseDB.Controllers
{
    public class UnitController : Controller
    {
        //
        // GET: /Unit/
        WarehouseRequestsRepositoryUnits Repository = new WarehouseRequestsRepositoryUnits();
        [HttpPost]
        public bool IsEventHistory(string id)
        {
            var res = Repository.GetRevisionListEvent(id);

            return res.Count > 0 ? true : false;
        }
        [HttpPost]
        public JsonResult GetUnits(PostRequest<RowCouch<EventCouch>> rep)
        {
            var res = Repository.GetUnits(); 
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}
