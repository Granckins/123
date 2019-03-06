using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Warehouse.Core.Repositories;
using Warehouse.Model;
using Warehouse.Model.Db;
using WarehouseDB.Filters;
using WebMatrix.WebData;

namespace WarehouseDB.Controllers
{
   
    public class WorkController : Controller
    {

        WarehouseRequestsRepository Repository = new WarehouseRequestsRepository();


        [HttpPost]
        public JsonResult GetDocuments(PostRequest<RowCouch<EventCouch>> rep)
        {
            var res = Repository.GetEventPaginDocuments(rep.page, rep.limit, rep.archive_str);
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
            var r1 = Repository.GetRevisionFiesldsEvent(id, res);
            return Json(r1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public bool IsEventHistory(string id)
        {
            var res = Repository.GetRevisionListEvent(id);

            return res.Count > 0 ? true : false ;
        }
        [HttpPost]
        public JsonResult ChangeEventDocument(RowCouch<EventCouch> res)
        {
         
            if (res.value != null)
            {
                if (res.value.Data_priyoma.Value.Date == new DateTime(1, 1, 1).Date)
                    res.value.Data_priyoma = null;
                if (res.value.Data_vydachi!=null&&res.value.Data_vydachi.Value.Date == new DateTime(1, 1, 1).Date)
                    res.value.Data_vydachi = null;
                if (res.value.Data_vydachi == null)
                    res.value.archive = false;
                var memberId = User.Identity.Name;
                var r = Repository.SetEventDocument(res.value, memberId, res.id);

                
            }
            var FS = new FilterSort();
            FS.FromStringToObject(res.filtername, res.filtervalue, res.sortname, res.sortvalue);
            if (FS.Filters.Count == 0)
            {
                if (FS.Sorts.Count > 0)
                {
                    if (FS.Sorts[0].name == "Дата приёма")
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(true, res.page, res.limit, res.archive_str);
                        return Json(res3, JsonRequestBehavior.AllowGet);
                    }
                    if (FS.Sorts[0].name == "Дата выдачи")
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(false, res.page, res.limit, res.archive_str);
                        return Json(res3, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var res1 = new CouchRequest<EventCouch>();
            if (FS.Filters.Count <= 2 && (res.datepr != ";" || res.datevd != ";"))
            {
                if (res.datepr != ";")
                    FS.FromStringToObject(true, res.datepr);
                if (res.datevd != ";")
                    FS.FromStringToObject(false, res.datevd);
                var fgfdg1 = new CouchRequest<EventCouch>();
                var fgfdg2 = new CouchRequest<EventCouch>();


                if (res.datepr != ";")
                    fgfdg1 = Repository.FilterByDateDocumentsCR(true, res.page, res.limit, res.archive_str, FS.datepr1, FS.datepr2);
                if (res.datevd != ";")
                    fgfdg2 = Repository.FilterByDateDocumentsCR(false, res.page, res.limit, res.archive_str, FS.datevd1, FS.datevd2);
                if (res.datepr != ";" && fgfdg1.rows.Count == 0)
                    fgfdg1 = fgfdg1;
                if (res.datevd != ";" && fgfdg2.rows.Count == 0)
                    fgfdg1 = fgfdg2;
                if ((res.datepr != ";" && fgfdg1.rows != null && fgfdg1.rows.Count == 0) && (res.datevd != ";" && fgfdg2.rows != null && fgfdg2.rows.Count == 0))
                {
                    res1 = Repository.CompareResultFilter(fgfdg1, fgfdg2);

                }
                else
                {
                    if (res.datepr != ";" && fgfdg1.rows.Count != 0)
                        fgfdg1 = fgfdg1;
                    if (res.datevd != ";" && fgfdg2.rows.Count != 0)
                        fgfdg1 = fgfdg2;
                    res1 = fgfdg1;
                }
                if (res1.total_rows == 0)
                    res1.total_rows = 1;
                return Json(res1, JsonRequestBehavior.AllowGet);
            }



            res1 = Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str, FS);


            return Json(res1, JsonRequestBehavior.AllowGet); 
        }
        [HttpPost]
        public JsonResult DeleteEventSubDocument(EditSubEvent sub)
        {
            var FS = new FilterSort();
            FS.FromStringToObject(sub.edit_event.filtername, sub.edit_event.filtervalue, "", "");
            sub.edit_event.value.Soderzhimoe.RemoveAt(sub.subidx);
            var memberId = User.Identity.Name;
            Repository.SetEventDocument(sub.edit_event.value, memberId, sub.edit_event.id);
            var rep = Repository.GetFilterSortDocuments(sub.edit_event.page, sub.edit_event.limit, sub.edit_event.archive_str, FS);
            return Json(rep, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FilterSortDocument(PostRequest<RowCouch<EventCouch>> res)
        {
            var FS = new FilterSort();
            FS.FromStringToObject(res.filtername, res.filtervalue, res.sortname, res.sortvalue);
            if (FS.Filters.Count == 0)
            {
                if (FS.Sorts.Count > 0)
                {
                    if (FS.Sorts[0].name == "Дата приёма"&&res.archive_str!=true)
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(true, res.page, res.limit, res.archive_str);
                        return Json(res3, JsonRequestBehavior.AllowGet);
                    }
                    if (FS.Sorts[0].name == "Дата выдачи" && res.archive_str != true)
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(false, res.page, res.limit, res.archive_str);
                        return Json(res3, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var res1 = new CouchRequest<EventCouch>();
            if (FS.Filters.Count <= 2 && (res.datepr != ";"  || res.datevd != ";"))
            {
                if (res.datepr != ";")
                    FS.FromStringToObject(true, res.datepr);
                if (res.datevd != ";")
                    FS.FromStringToObject(false, res.datevd);
                var fgfdg1 = new CouchRequest<EventCouch>();
                var fgfdg2 = new CouchRequest<EventCouch>();


                if (res.datepr != ";")
                    fgfdg1 = Repository.FilterByDateDocumentsCR(true, res.page, res.limit, res.archive_str, FS.datepr1, FS.datepr2);
                if (res.datevd != ";")
                    fgfdg2 = Repository.FilterByDateDocumentsCR(false, res.page, res.limit, res.archive_str, FS.datevd1, FS.datevd2);
                if (res.datepr != ";" && fgfdg1.rows.Count == 0)
                    fgfdg1 = fgfdg1;
                if (res.datevd != ";" && fgfdg2.rows.Count == 0)
                    fgfdg1 = fgfdg2;
                if ((res.datepr != ";" && fgfdg1.rows != null && fgfdg1.rows.Count == 0) && (res.datevd != ";" && fgfdg2.rows != null && fgfdg2.rows.Count == 0))
                {
                    res1 = Repository.CompareResultFilter(fgfdg1, fgfdg2);

                }
                else
                {
                    if (res.datepr != ";" && fgfdg1.rows.Count != 0)
                        fgfdg1 = fgfdg1;
                    if (res.datevd != ";" && fgfdg2.rows.Count != 0)
                        fgfdg1 = fgfdg2;
                    res1 = fgfdg1;
                }
                if(res1.total_rows==0)
                    res1.total_rows=1;
                return Json(res1, JsonRequestBehavior.AllowGet);
            }



            res1 = Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str, FS);


            return Json(res1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteEventDocument(PostRequest<RowCouch<EventCouch>> res)
        {
           
            var f = Repository.DeleteEventDocument(res.entity.value, res.entity.id);

            var FS = new FilterSort();
            FS.FromStringToObject(res.filtername, res.filtervalue, res.sortname, res.sortvalue);
            if (FS.Filters.Count == 0)
            {
                if (FS.Sorts.Count > 0)
                {
                    if (FS.Sorts[0].name == "Дата приёма" && res.archive_str != true)
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(true, res.page, res.limit, res.archive_str);
                        return Json(res3, JsonRequestBehavior.AllowGet);
                    }
                    if (FS.Sorts[0].name == "Дата выдачи" && res.archive_str != true)
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(false, res.page, res.limit, res.archive_str);
                        return Json(res3, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var res1 = new CouchRequest<EventCouch>();
            if (FS.Filters.Count <= 2 && (res.datepr != ";" || res.datevd != ";"))
            {
                if (res.datepr != ";")
                    FS.FromStringToObject(true, res.datepr);
                if (res.datevd != ";")
                    FS.FromStringToObject(false, res.datevd);
                var fgfdg1 = new CouchRequest<EventCouch>();
                var fgfdg2 = new CouchRequest<EventCouch>();


                if (res.datepr != ";")
                    fgfdg1 = Repository.FilterByDateDocumentsCR(true, res.page, res.limit, res.archive_str, FS.datepr1, FS.datepr2);
                if (res.datevd != ";")
                    fgfdg2 = Repository.FilterByDateDocumentsCR(false, res.page, res.limit, res.archive_str, FS.datevd1, FS.datevd2);
                if (res.datepr != ";" && fgfdg1.rows.Count == 0)
                    fgfdg1 = fgfdg1;
                if (res.datevd != ";" && fgfdg2.rows.Count == 0)
                    fgfdg1 = fgfdg2;
                if ((res.datepr != ";" && fgfdg1.rows != null && fgfdg1.rows.Count == 0) && (res.datevd != ";" && fgfdg2.rows != null && fgfdg2.rows.Count == 0))
                {
                    res1 = Repository.CompareResultFilter(fgfdg1, fgfdg2);

                }
                else
                {
                    if (res.datepr != ";" && fgfdg1.rows.Count != 0)
                        fgfdg1 = fgfdg1;
                    if (res.datevd != ";" && fgfdg2.rows.Count != 0)
                        fgfdg1 = fgfdg2;
                    res1 = fgfdg1;
                }
                if (res1.total_rows == 0)
                    res1.total_rows = 1;
                return Json(res1, JsonRequestBehavior.AllowGet);
            }



            res1 = Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str, FS);

            return Json(res1, JsonRequestBehavior.AllowGet);
        }
       [HttpPost]
        public FileResult DownloadData(PostRequest<RowCouch<EventCouch>> res)
        {
            var FS = new FilterSort();
            res.page = 1;
            res.limit = 1;
            FS.FromStringToObject(res.filtername, res.filtervalue, res.sortname, res.sortvalue);
            if (FS.Filters.Count == 0)
            {
                if (FS.Sorts.Count > 0)
                {
                    if (FS.Sorts[0].name == "Дата приёма" && res.archive_str != true)
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(true, res.page, res.limit, res.archive_str);
                        var total = res3.total_rows;
                        string output4 = "";
                        output4 += "Номер упаковки;Наименование изделия;Заводской номер;Количество;Местонахождение на складе;Обозначение;Содержимое;Система;Ответственный;Принадлежность;Дата приёма;Откуда;Дата выдачи;Куда;Номер пломбы;Стоимость;Вес брутто;Вес нетто;Длина;Ширина;Высота;Примечание;Добавил\n";
                        if (total > 1)
                        {
                              res3 = Repository.OrderByDateDocumentsCR(true, res.page,total, res.archive_str);
                            foreach (var s in res3.rows)
                            {
                               
                                output4 += s.value.ToString();
                            }
                        }


                        string lvCSV = output4;

                        string lvFileName = "myfile.csv";

                        // UTF8 with BOM (byte order mark)
                        UTF8Encoding lvUtf8EncodingWithBOM = new UTF8Encoding(true, true);

                        // Byte Order Mark -  \x00EF\x00BB\x00BF
                        string lvBOM =
                        Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

                        return File(lvUtf8EncodingWithBOM.GetBytes(lvBOM + lvCSV), "text/csv;charset=utf-8", lvFileName);
                    }
                    if (FS.Sorts[0].name == "Дата выдачи" && res.archive_str != true)
                    {
                        var res3 = Repository.OrderByDateDocumentsCR(false, res.page, res.limit, res.archive_str);
                        var total = res3.total_rows;
                        string output4 = "";
                        output4 += "Номер упаковки;Наименование изделия;Заводской номер;Количество;Местонахождение на складе;Обозначение;Содержимое;Система;Ответственный;Принадлежность;Дата приёма;Откуда;Дата выдачи;Куда;Номер пломбы;Стоимость;Вес брутто;Вес нетто;Длина;Ширина;Высота;Примечание;Добавил\n";
                        if (total > 1)
                        {
                            res3 = Repository.OrderByDateDocumentsCR(false, res.page,total, res.archive_str);
                            foreach (var s in res3.rows)
                            {
                                
                                output4 += s.value.ToString();
                            }
                        }


                        string lvCSV = output4;

                        string lvFileName = "myfile.csv";

                        // UTF8 with BOM (byte order mark)
                        UTF8Encoding lvUtf8EncodingWithBOM = new UTF8Encoding(true, true);

                        // Byte Order Mark -  \x00EF\x00BB\x00BF
                        string lvBOM =
                        Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

                        return File(lvUtf8EncodingWithBOM.GetBytes(lvBOM + lvCSV), "text/csv;charset=utf-8", lvFileName);
                    }
                }
            }
            var res1 = new CouchRequest<EventCouch>();
            if (FS.Filters.Count <= 2 && (res.datepr != ";" || res.datevd != ";"))
            {
                if (res.datepr != ";")
                    FS.FromStringToObject(true, res.datepr);
                if (res.datevd != ";")
                    FS.FromStringToObject(false, res.datevd);
                var fgfdg1 = new CouchRequest<EventCouch>();
                var fgfdg2 = new CouchRequest<EventCouch>();


                if (res.datepr != ";")
                    fgfdg1 = Repository.FilterByDateDocumentsCR(true, res.page, res.limit, res.archive_str, FS.datepr1, FS.datepr2);
                if (res.datevd != ";")
                    fgfdg2 = Repository.FilterByDateDocumentsCR(false, res.page, res.limit, res.archive_str, FS.datevd1, FS.datevd2);
                if (res.datepr != ";" && fgfdg1.rows.Count == 0)
                    fgfdg1 = fgfdg1;
                if (res.datevd != ";" && fgfdg2.rows.Count == 0)
                    fgfdg1 = fgfdg2;
                if ((res.datepr != ";" && fgfdg1.rows != null && fgfdg1.rows.Count == 0) && (res.datevd != ";" && fgfdg2.rows != null && fgfdg2.rows.Count == 0))
                {
                    res1 = Repository.CompareResultFilter(fgfdg1, fgfdg2);

                }
                else
                {
                    if (res.datepr != ";" && fgfdg1.rows.Count != 0)
                        fgfdg1 = fgfdg1;
                    if (res.datevd != ";" && fgfdg2.rows.Count != 0)
                        fgfdg1 = fgfdg2;
                    res1 = fgfdg1;
                }
                if (res1.total_rows == 0)
                    res1.total_rows = 1;
               
                 
                var total = res1.total_rows;
                string output4 = "";
                output4 += "Номер упаковки;Наименование изделия;Заводской номер;Количество;Местонахождение на складе;Обозначение;Содержимое;Система;Ответственный;Принадлежность;Дата приёма;Откуда;Дата выдачи;Куда;Номер пломбы;Стоимость;Вес брутто;Вес нетто;Длина;Ширина;Высота;Примечание;Добавил\n";
                if (total > 1)
                {
                    if (res.datepr != ";")
                        FS.FromStringToObject(true, res.datepr);
                    if (res.datevd != ";")
                        FS.FromStringToObject(false, res.datevd);
                    fgfdg1 = new CouchRequest<EventCouch>();
                    fgfdg2 = new CouchRequest<EventCouch>();


                    if (res.datepr != ";")
                        fgfdg1 = Repository.FilterByDateDocumentsCR(true, res.page, total, res.archive_str, FS.datepr1, FS.datepr2);
                    if (res.datevd != ";")
                        fgfdg2 = Repository.FilterByDateDocumentsCR(false, res.page, total, res.archive_str, FS.datevd1, FS.datevd2);
                    if (res.datepr != ";" && fgfdg1.rows.Count == 0)
                        fgfdg1 = fgfdg1;
                    if (res.datevd != ";" && fgfdg2.rows.Count == 0)
                        fgfdg1 = fgfdg2;
                    if ((res.datepr != ";" && fgfdg1.rows != null && fgfdg1.rows.Count == 0) && (res.datevd != ";" && fgfdg2.rows != null && fgfdg2.rows.Count == 0))
                    {
                        res1 = Repository.CompareResultFilter(fgfdg1, fgfdg2);

                    }
                    else
                    {
                        if (res.datepr != ";" && fgfdg1.rows.Count != 0)
                            fgfdg1 = fgfdg1;
                        if (res.datevd != ";" && fgfdg2.rows.Count != 0)
                            fgfdg1 = fgfdg2;
                        res1 = fgfdg1;
                    }
                    if (res1.total_rows == 0)
                        res1.total_rows = 1;
               
                    foreach (var s in res1.rows)
                    {
                      
                        output4 += s.value.ToString();
                    }
                }


                string lvCSV = output4;

                string lvFileName = "myfile.csv";

                // UTF8 with BOM (byte order mark)
                UTF8Encoding lvUtf8EncodingWithBOM = new UTF8Encoding(true, true);

                // Byte Order Mark -  \x00EF\x00BB\x00BF
                string lvBOM =
                Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

                return File(lvUtf8EncodingWithBOM.GetBytes(lvBOM + lvCSV), "text/csv;charset=utf-8", lvFileName);
            }



            res1 = Repository.GetFilterSortDocuments(res.page, res.limit, res.archive_str, FS);
            var total1 = res1.total_rows;
            string output = "";
            output += "Номер упаковки;Наименование изделия;Заводской номер;Количество;Местонахождение на складе;Обозначение;Содержимое;Система;Ответственный;Принадлежность;Дата приёма;Откуда;Дата выдачи;Куда;Номер пломбы;Стоимость;Вес брутто;Вес нетто;Длина;Ширина;Высота;Примечание;Добавил\n";
            if (total1>1)
            {
                res1 = Repository.GetFilterSortDocuments(res.page, total1, res.archive_str, FS);
                foreach (var s in res1.rows)
                {  
                    output += s.value.ToString();
                }
            }
          
            
           string lvCSV1 = output;

           string lvFileName1 = "myfile.csv";

           // UTF8 with BOM (byte order mark)
           UTF8Encoding lvUtf8EncodingWithBOM1 = new UTF8Encoding(true, true);

           // Byte Order Mark -  \x00EF\x00BB\x00BF
           string lvBOM1 =
           Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());

           return File(lvUtf8EncodingWithBOM1.GetBytes(lvBOM1 + lvCSV1), "text/csv;charset=utf-8", lvFileName1);
        }
    }
}