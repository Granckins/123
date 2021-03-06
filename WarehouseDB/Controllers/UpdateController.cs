﻿using CsvHelper;
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
            var we = new EventCouch();

            foreach (var e in ListE)
            {
                //найти есть ли _id уже в базе
                var dsd = Repository.GetEventDocument(e.value._id);
                // _id нет в базе
                if (dsd._id == null)
                {   // в добавляемом есть ревизии
                    if (e.value._revs.Count > 1)
                    {
                        int i = 0;
                        int count = e.value._revs.Count;
                        for (i = count - 1; i > 0; i--)
                        {
                            if (i != count - 1)
                                Repository.SetEventDocument(e.value._revs[i], e.value.Dobavil, e.value._id);
                            else
                                Repository.UpdateEventDocument(e.value._revs[i], e.value._revs[i].Dobavil, e.value._revs[i]._id);

                        }
                    }
                    else
                        Repository.UpdateEventDocument(EventManager.ConvertEventCouchFullToEventCouch(e.value), e.value.Dobavil, e.value._id);
                }
                else
                {
                    // получение в существующем БД ревизий
                    var revs_info = Repository.GetRevisionListEvent(dsd._id, false);
                   
                    if (e.value._revs.Count > 0)
                    {
                        int i = 0;
                        int count = e.value._revs.Count;
                        for (i = count-1 ; i >=0; i--)
                        {
                            // получение полей ревизий записи существующей в БД
                            var revis = Repository.GetRevisionFiesldsEvent(dsd._id, revs_info);
                            // получение полей ревизий записи существующей в БД

                         var hy=   Repository.CompareEvent(revis.ToEventCouch(), e.value._revs[i]);
                            if(!hy)
                                Repository.SetEventDocument(e.value._revs[i], e.value.Dobavil, e.value._id);
                             

                        }
                        Repository.SetEventDocument(EventManager.ConvertEventCouchFullToEventCouch(e.value), e.value.Dobavil, e.value._id);
                    }
                    else
                        Repository.SetEventDocument(EventManager.ConvertEventCouchFullToEventCouch(e.value), e.value.Dobavil, e.value._id);

                }
            }
 
            var r = "";
           return Json(r);
        }
        
    }
}
