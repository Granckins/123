﻿using System;
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
          [AcceptVerbs(HttpVerbs.Post)]
          public JsonResult FilterSortDocument (PostRequest<SelectParametrs> res)
          {
              return null;
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
                  var memberId =  User.Identity.Name ;
                 var r=Repository.SetEventDocument(res.value,memberId, res.id);
                  var rep= new EventCouch();
                  if (res.id == null)
                  {
                      if (r.Count > 0 && !r.Any(x => x.id == "000000000000000000000000"))
                      {
                          rep = Repository.GetEventDocument(r.First().id);

                          return Json(rep, JsonRequestBehavior.AllowGet);
                      }
                      else
                      {
                          EventCouch error = new EventCouch() { _id = "000000000000000000000000" };
                          return Json(error, JsonRequestBehavior.AllowGet);
                      }
                    
                  }
                  else
                  {
                      if (r.Count > 0)
                      {
                          if (!r.Any(x => x.id == "000000000000000000000000"))
                          {
                              rep = Repository.GetEventDocument(res.id);
                              return Json(rep, JsonRequestBehavior.AllowGet);
                          }
                          else
                          {
                              rep = Repository.GetEventDocument(res.id);
                              rep._id = "000000000000000000000000"; 
                              return Json(rep, JsonRequestBehavior.AllowGet);
                          }
                      }
                      else return null;
                       
                         
                     
                  }
                
              }
              else return null;
          }
        [HttpPost]
        public JsonResult DeleteEventSubDocument(EditSubEvent sub)
        {
            sub.edit_event.value.Soderzhimoe.RemoveAt(sub.subidx);
            var memberId = User.Identity.Name;
            Repository.SetEventDocument(sub.edit_event.value,memberId, sub.edit_event.id);
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
