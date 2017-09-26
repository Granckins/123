using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;
using Warehouse.Model;
using Warehouse.Model.Db;

namespace WarehouseDB.Controllers
{
    public class WorkController : Controller
    {
       
        WarehouseRequestsRepository Repository = new WarehouseRequestsRepository();


          [HttpGet]
        public JsonResult GetDocuments(int page = 1, int limit = 10)
        {
          var res=  Repository.GetEventPaginDocuments();
          return Json(res, JsonRequestBehavior.AllowGet);
        }
          [HttpGet]
          public JsonResult GetEventDocument(string id)
          {
              var res = Repository.GetEventDocument(id);
              return Json(res, JsonRequestBehavior.AllowGet);
          }
        [HttpPost]
          public JsonResult ChangeEventDocument(RowCouch<EventCouch> res)
          {
               Repository.SetEventDocument(res.value, res.id);
               var rep = Repository.GetEventDocument(res.id);
              return Json(rep, JsonRequestBehavior.AllowGet);
          }
        [HttpPost]
        public JsonResult DeleteEventSubDocument(EditSubEvent sub)
        {
            sub.edit_event.value.Soderzhimoe.RemoveAt(sub.subidx);
            Repository.SetEventDocument(sub.edit_event.value, sub.edit_event.id);
            var rep = Repository.GetEventDocument(sub.edit_event.id);
            return Json(rep, JsonRequestBehavior.AllowGet);
        }
    }
}
