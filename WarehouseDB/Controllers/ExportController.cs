using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;
using Warehouse.Model;
using Warehouse.Model.Db;
using WarehouseDB.Filters;
using WebMatrix.WebData;


namespace WarehouseDB.Controllers
{
    public class ExportController : Controller
    {
        WarehouseRequestsRepository Repository = new WarehouseRequestsRepository();

        [HttpPost]
        public FileContentResult ExportDocument(PostRequestExport res)
        {
             
            var res1 = new CouchRequest<EventCouchFull>();
            string dateiz1="", dateiz2="";
            if (res.dateiz != ";"&& dateiz1!=""&& dateiz2!="")
            {
                 
                    var Filters = new List<string>();
                    try { Filters = res.dateiz.Split(';').ToList(); }
                    catch (Exception e)
                    {
                        Filters = new List<string>();
                    } 
                        dateiz1 = Filters[0];
                        dateiz2 = Filters[1]; 
                            
              
            }
            if(res.all_events)
                res1 = Repository.GetAllEvents();
            else
            res1 = Repository.FilterByDateIzmen(dateiz1,dateiz2);
            foreach (var rr in res1.rows)
            {
                var revs = Repository.GetRevisionListEvent(rr.id);
                var r1 = Repository.GetRevisionFiesldsEvent(rr.id, revs);
                rr.value._revs = r1.ToEventCouch();
                rr.value._id = rr.id;
            }
            string output = JsonConvert.SerializeObject(res1.rows);
            byte[] contents = System.Text.Encoding.UTF8.GetBytes(output);
             
            return File(contents, "application/json", "test.json");
            
        }
    }
}