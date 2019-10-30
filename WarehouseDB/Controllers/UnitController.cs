using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet]
        public FileResult GetAttachmentById(string id, int number = 0)
{
   var res = Repository.GetAttachmentById(id,number);
  

        if (res!=null)
        {
              return File(res, "image/jpeg");
        
        }

        //file is empty, so return null
        return null;
 
 
}
         
    }
}
