using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;
using Warehouse.Model;
using Warehouse.Model.Db;
using WarehouseDB.Filters;
using WebMatrix.WebData;

namespace WarehouseDB.Controllers
{
    [InitializeSimpleMembership]
    public class WorkController : Controller
    {
       
        WarehouseRequestsRepository Repository = new WarehouseRequestsRepository();


          [HttpPost]
        public JsonResult GetDocuments(PostRequest<RowCouch<EventCouch>> rep)
        {
          var res=  Repository.GetEventPaginDocuments(rep.page,rep.limit,rep.archive_str);
          return Json(res, JsonRequestBehavior.AllowGet);
        }
          [HttpGet]
          public JsonResult GetEventDocument(string id)
          {
              var res = Repository.GetEventDocument(id);
              return Json(res, JsonRequestBehavior.AllowGet);
          }
          [HttpGet]
          public JsonResult GetEventHistory(string id)
          {
              var res = Repository.GetRevisionListEvent(id);
              var r1=Repository.GetRevisionFiesldsEvent(id,res);
              return Json(r1, JsonRequestBehavior.AllowGet);
          }
          [HttpGet]
          public JsonResult IsEventHistory(string id)
          {
              var res = Repository.GetRevisionListEvent(id); 
              
              return Json(res.Count, JsonRequestBehavior.AllowGet);
          }
        [HttpPost]
          public JsonResult ChangeEventDocument(RowCouch<EventCouch> res)
          {
              var FS = new FilterSort();
              FS.FromStringToObject(res.filtername, res.filtervalue, "", "");
              if (res.value != null)
              {
                  if (res.value.Data_priyoma == new DateTime(1, 1, 1))
                      res.value.Data_priyoma = null;
                  if (res.value.Data_priyoma == new DateTime(1, 1, 1))
                      res.value.Data_priyoma = null;
                  var memberId =  User.Identity.Name ;
                 var r=Repository.SetEventDocument(res.value,memberId, res.id);

                 var res1 = Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str, FS);
                 return Json(res1, JsonRequestBehavior.AllowGet);
              }
              else
              {
                  var res1 = Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str, FS);
                  return Json(res1, JsonRequestBehavior.AllowGet);
              }
          }
        [HttpPost]
        public JsonResult DeleteEventSubDocument(EditSubEvent sub)
        {
            var FS = new FilterSort();
            FS.FromStringToObject(sub.edit_event.filtername, sub.edit_event.filtervalue, "", "");
            sub.edit_event.value.Soderzhimoe.RemoveAt(sub.subidx);
            var memberId = User.Identity.Name;
            Repository.SetEventDocument(sub.edit_event.value,memberId, sub.edit_event.id);
            var rep = Repository.GetFilterSortDocuments(sub.edit_event.page, sub.edit_event.limit, sub.edit_event.archive_str, FS);
            return Json(rep, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FilterSortDocument(PostRequest<RowCouch<EventCouch>> res)
        {
            var FS = new FilterSort();
            FS.FromStringToObject(res.filtername,res.filtervalue,"","");

      //     var fgfdg=Repository.FilterByDateDocuments(true,res.page, res.limit, res.archive_str,"2017-1-1","2019-1-1");

            var res1=Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str,FS);
            return Json(res1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteEventDocument(PostRequest<RowCouch<EventCouch>> res)
        {
            var FS = new FilterSort();
            FS.FromStringToObject(res.filtername, res.filtervalue, "", "");
          var f=  Repository.DeleteEventDocument(res.entity.value,res.entity.id);
          var rep = Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str, FS); 
            
                return Json(rep, JsonRequestBehavior.AllowGet); 
        }
    }
}
