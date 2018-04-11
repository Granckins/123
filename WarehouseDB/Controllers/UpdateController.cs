using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;
using Warehouse.Model;
using Warehouse.Model.Db;
using WarehouseDB.Models;
using WarehouseDB.Services;

namespace WarehouseDB.Controllers
{
    public class UpdateController : Controller
    {
        WarehouseRequestsRepository Repository = new WarehouseRequestsRepository();

      
        // GET: /Upload/ 
        [HttpPost]
        public JsonResult Upload( )
        {
            HttpFileCollectionBase files = Request.Files;


            HttpPostedFileBase uploadedFile = files[0];
            Stream fileStream = uploadedFile.InputStream;
            BinaryReader b = new BinaryReader(fileStream);
            byte[] binData = b.ReadBytes(uploadedFile.ContentLength); 
            string result = System.Text.Encoding.UTF8.GetString(binData);
            List<ImportResultResponse> list = new List<ImportResultResponse>();
            List<RowCouch<EventCouchFull>> ListE = new List<RowCouch<EventCouchFull>>();
              ListE = JsonConvert.DeserializeObject<List<RowCouch<EventCouchFull>>>(result);

            var r = "";
           return Json(r);
        }
        
    }
}
