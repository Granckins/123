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
using Warehouse.Model.Db;
using WarehouseDB.Models;
using WarehouseDB.Services;

namespace WarehouseDB.Controllers
{
    public class UploadController : Controller
    {
        WarehouseRequestsRepository Repository = new WarehouseRequestsRepository();

        [HttpPost]
        public JsonResult UploadPreview(   )
        {
            HttpFileCollectionBase files = Request.Files;


            HttpPostedFileBase uploadedFile = files[0];
            Stream fileStream = uploadedFile.InputStream;

            System.Collections.Generic.List<EventWar> ListE = new List<EventWar>();
            using (StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding(1251)))
            {
                var csv = new CsvReader(sr);
                csv.Configuration.Delimiter = ";";
                var model = new EventWar();
                while (csv.Read())
                {

                    model = new EventWar();
                    try
                    {
                        model.Номер_упаковки = csv.GetField<string>(0) == "" ? 0 : csv.GetField<int>(0);
                        model.Наименование_изделия = csv.GetField<string>(1);
                        model.Заводской_номер = csv.GetField<string>(2);
                        model.Количество = csv.GetField<string>(3) == "" ? 0 : csv.GetField<int>(3);
                        model.Обозначение = csv.GetField<string>(4);
                        model.Наименование_составной_единицы = csv.GetField<string>(5);
                        model.Обозначение_составной_единицы = csv.GetField<string>(6);
                        model.Количество_составных_единиц = csv.GetField<string>(7) == "" ? 0 : csv.GetField<int>(7);
                        model.Система = csv.GetField<string>(8);
                        model.Принадлежность = csv.GetField<string>(9);
                        model.Стоимость = csv.GetField<string>(10) == "" ? 0 : csv.GetField<float>(11);
                        model.Ответственный = csv.GetField<string>(11);
                        model.Местонахождение_на_складе = csv.GetField<string>(12);
                        model.Вес_брутто = csv.GetField<string>(13) == "" ? 0 : csv.GetField<float>(13);
                        model.Вес_нетто = csv.GetField<string>(14) == "" ? 0 : csv.GetField<float>(14);
                        model.Длина = csv.GetField<string>(15) == "" ? 0 : csv.GetField<float>(15);
                        model.Ширина = csv.GetField<string>(16) == "" ? 0 : csv.GetField<float>(16);
                        model.Высота = csv.GetField<string>(17) == "" ? 0 : csv.GetField<float>(17);

                        model.Дата_приёма = csv.GetField<string>(18) == "" ? new DateTime() : csv.GetField<DateTime>(18);
                        model.Откуда = csv.GetField<string>(19);
                        model.Дата_выдачи = csv.GetField<string>(20) == "" ? new DateTime() : csv.GetField<DateTime>(20);
                        model.Куда = csv.GetField<string>(21);
                        model.Номер_пломбы = csv.GetField<string>(22);
                        model.Примечание = csv.GetField<string>(23);
                       
                        ListE.Add(model);
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            // string output = JsonConvert.SerializeObject(ListE);
            var flag = false;
            var SecondParent = false;
            var child = false;
            var bufList = new List<EventWar>();
            var CouchDataSet = new List<EventCouch>();
            foreach (var e in ListE)
            {
                if (e.Наименование_составной_единицы == "")
                {
                    if (flag == false && SecondParent == true)
                    {
                        CouchDataSet.Add(EventManager.ConvertEventWarListToEventCouch(bufList));
                        bufList.Clear();
                        bufList.Add(e);
                        SecondParent = true;
                    }
                    else
                    {
                        if (child == false)
                        {
                            bufList.Add(e);
                            SecondParent = true;
                        }
                        else
                        {
                            CouchDataSet.Add(EventManager.ConvertEventWarListToEventCouch(bufList));
                            bufList.Clear();
                            bufList.Add(e);
                            SecondParent = true;
                            flag = false;
                        }
                    }
                    flag = false;
                }
                else
                {
                    if (bufList.First().Наименование_составной_единицы == "")
                    {
                        flag = true;
                        SecondParent = false;
                        child = true;
                        bufList.Add(e);
                    }
                }
            }
            if (bufList.Count > 0)
                CouchDataSet.Add(EventManager.ConvertEventWarListToEventCouch(bufList));
            var dsdfs = Json(CouchDataSet, "application/json", JsonRequestBehavior.AllowGet);
            return Json(dsdfs);
        }
       
        // GET: /Upload/ 
        [HttpPost]
        public JsonResult Upload( )
        {
            HttpFileCollectionBase files = Request.Files;


            HttpPostedFileBase uploadedFile = files[0];
            Stream fileStream = uploadedFile.InputStream;

            System.Collections.Generic.List<EventWar> ListE = new List<EventWar>();
            using (StreamReader sr = new StreamReader(fileStream, Encoding.GetEncoding(1251)))
            {
                var csv = new CsvReader(sr);
                csv.Configuration.Delimiter = ";";
                var model = new EventWar();
                while (csv.Read())
                {

                    model = new EventWar();
                    try
                    {
                        model.Номер_упаковки = csv.GetField<int>(0);
                        model.Наименование_изделия = csv.GetField<string>(1);
                        model.Заводской_номер = csv.GetField<string>(2);
                        model.Количество = csv.GetField<string>(3) == "" ? 0 : csv.GetField<int>(3);
                        model.Обозначение = csv.GetField<string>(4);
                        model.Наименование_составной_единицы = csv.GetField<string>(5);
                        model.Обозначение_составной_единицы = csv.GetField<string>(6);
                        model.Количество_составных_единиц = csv.GetField<string>(7) == "" ? 0 : csv.GetField<int>(7);
                        model.Система = csv.GetField<string>(8);
                        model.Принадлежность = csv.GetField<string>(9);
                         model.Стоимость = csv.GetField<string>(11) == "" ? 0 : csv.GetField<float>(11);
                        model.Ответственный = csv.GetField<string>(12);
                        model.Местонахождение_на_складе = csv.GetField<string>(13);
                        model.Вес_брутто = csv.GetField<string>(14) == "" ? 0 : csv.GetField<float>(14);
                        model.Вес_нетто = csv.GetField<string>(15) == "" ? 0 : csv.GetField<float>(15);
                        model.Длина = csv.GetField<string>(16) == "" ? 0 : csv.GetField<float>(16);
                        model.Ширина = csv.GetField<string>(17) == "" ? 0 : csv.GetField<float>(17);
                        model.Высота = csv.GetField<string>(18) == "" ? 0 : csv.GetField<float>(18);
                        model.Номер_контейнера = csv.GetField<string>(19);
                        model.Номер_упаковочного_ящика = csv.GetField<string>(20);
                        model.Дата_приёма = csv.GetField<string>(21) == "" ? new DateTime() : csv.GetField<DateTime>(21);
                        model.Откуда = csv.GetField<string>(22);
                        model.Дата_выдачи = csv.GetField<string>(23) == "" ? new DateTime() : csv.GetField<DateTime>(21);
                        model.Куда = csv.GetField<string>(24);
                        model.Номер_пломбы = csv.GetField<string>(25);
                        model.Примечание = csv.GetField<string>(26);
                        ListE.Add(model);
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            // string output = JsonConvert.SerializeObject(ListE);
            var flag = false;
            var SecondParent = false;
            var child = false;
            var bufList = new List<EventWar>();
            var CouchDataSet = new List<EventCouch>();
            foreach (var e in ListE)
            {
                if (e.Наименование_составной_единицы == "")
                {
                    if (flag == false && SecondParent == true)
                    {
                        CouchDataSet.Add(EventManager.ConvertEventWarListToEventCouch(bufList));
                        bufList.Clear();
                        bufList.Add(e);
                        SecondParent = true;
                    }
                    else
                    {
                        if (child == false)
                        {
                            bufList.Add(e);
                            SecondParent = true;
                        }
                        else
                        {
                            CouchDataSet.Add(EventManager.ConvertEventWarListToEventCouch(bufList));
                            bufList.Clear();
                            bufList.Add(e);
                            SecondParent = true;
                            flag = false;
                        }
                    }
                    flag = false;
                }
                else
                {
                    if (bufList.First().Наименование_составной_единицы == "")
                    {
                        flag = true;
                        SecondParent = false;
                        child = true;
                        bufList.Add(e);
                    }
                }
            }
            if(bufList.Count>0)
                CouchDataSet.Add(EventManager.ConvertEventWarListToEventCouch(bufList));
            var dsdfs = Json(CouchDataSet, "application/json", JsonRequestBehavior.AllowGet);
         

           var str=JsonConvert.SerializeObject(CouchDataSet);
           var memberId = User.Identity.Name;
           var resp = Repository.SetEventDocuments(CouchDataSet, memberId);
        
            
           return Json(resp);
        }
        [HttpPost]
        public FineUploaderResult UploadFile(FineUpload upload, string extraParam1 = null, int extraParam2 = 0)
        {
            // asp.net mvc will set extraParam1 and extraParam2 from the params object passed by Fine-Uploader

            var dir = Server.MapPath("../Content/images");

            var filePath = Path.Combine(dir, upload.Filename);

            try
            {

                upload.SaveAs(filePath);


                return new FineUploaderResult(true, new { FilePath = filePath });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }

            // the anonymous object in the result below will be convert to json and set back to the browser
            return new FineUploaderResult(true, new { extraInformation = 12345 });
        }
    }
}
