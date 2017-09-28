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
          var res=  Repository.GetEventPaginDocuments(page,limit);
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
         
              if (res.value != null)
              {
                  if (res.value.Data_priyoma == new DateTime(1, 1, 1))
                      res.value.Data_priyoma = null;
                  if (res.value.Data_priyoma == new DateTime(1, 1, 1))
                      res.value.Data_priyoma = null;
                 var r=Repository.SetEventDocument(res.value, res.id);
                  var rep= new EventCouch();
                  if(res.id==null)
   rep = Repository.GetEventDocument(r.First().id);
                  else
                   rep = Repository.GetEventDocument(res.id);
                 
                  return Json(rep, JsonRequestBehavior.AllowGet);
              }
              else return null;
          }
        [HttpPost]
        public JsonResult DeleteEventSubDocument(EditSubEvent sub)
        {
            sub.edit_event.value.Soderzhimoe.RemoveAt(sub.subidx);
            Repository.SetEventDocument(sub.edit_event.value, sub.edit_event.id);
            var rep = Repository.GetEventDocument(sub.edit_event.id);
            return Json(rep, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteEventDocument(PostRequest<RowCouch<EventCouch>> res)
        {

          var f=  Repository.DeleteEventDocument(res.entity.value,res.entity.id);
            var rep = Repository.GetEventPaginDocuments();
            if (f == true)
                return Json(rep, JsonRequestBehavior.AllowGet);
            else
                return null;
          //  return Json("");
        }
    }
}
