using LoveSeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Core;
using Warehouse.Model;
using Warehouse.Model.Db;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Xml; 
namespace Warehouse.Core.Repositories
{
    public class WarehouseRequestsRepository : IRepositoryRequests
    {
         
        public UserIdentity GetUserIdentityByName(string username)
        {

            var url = "http://localhost:5984/_fti/local/users/_design/foo/by_username?q=UserName:" + "\"" + username + "\"^10";
            var request = (HttpWebRequest)WebRequest.Create(url);
             
            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();
          
            var user = new User();
            using(var responseStream=response.GetResponseStream()){
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                 var lucene = JsonConvert.DeserializeObject<LuceneRequest<User>>(res);

                 if (lucene.rows.Count <=0)
                 {
                     return new UserIdentity { UserId = "000000000000000000000000" };
                 }
                 user = lucene.rows.First().fields;
                 user.Id = lucene.rows.First().id;
            }
             
            return new UserIdentity
            {
                UserId = user.Id,
                Name = user.UserName,
                Password = user.Password,

            };
        }
        public List<string> GetUserRoles(UserIdentity user)
        {
           // var url = "http://localhost:5984/_fti/local/users/_design/foo/by_id?q=_id:" + user.UserId;
           // var request = (HttpWebRequest)WebRequest.Create(url);

           // request.Credentials = new NetworkCredential("admin", "root");
           // var response = request.GetResponse();

           //var userl = new User();
           // using (var responseStream = response.GetResponseStream())
           // {
           //     var reader = new StreamReader(responseStream, Encoding.UTF8);
           //     var res = reader.ReadToEnd();
           //     var lucene = JsonConvert.DeserializeObject<LuceneRequest<User>>(res);
           //     userl = lucene.rows.First().fields; 
           // }

           // var roles = userl.Roles;
           
           //var rolesDict = Roles.GetInstance().List;
           //return roles.Select(r => rolesDict[r]).ToList();
            return new List<string>();
             
        }
        public string GetUUID()
        {
            var url = "http://localhost:5984/_uuids";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();

            try {
                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream, Encoding.UTF8);
                    var res = reader.ReadToEnd();

                    var uuid = JsonConvert.DeserializeObject<UUID>(res);
                    return uuid.uuids.First();
                }
            }
            catch (Exception e)
            {

            }
           

           
            return null;
        }
        public bool DeleteEventDocument(EventCouch CouchDataSet, string id)
        {
            List<ImportResultResponse> list = new List<ImportResultResponse>();
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:5984/events/" + id + "?rev=" + CouchDataSet._rev);

            ServicePointManager.DefaultConnectionLimit = 1000; 
            request.Credentials = new NetworkCredential("admin", "root");
            request.Method = "DELETE"; 
            request.KeepAlive = false;
            var response = request.GetResponse();
             
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<DeleteCouchResp>(res);

                return lucene.ok;
            }
             

            return false;
        }
        private bool CompareEvent(EventCouch CouchDataSet, string id)
        {
            var last=GetEventDocument(id);
            var comp = EventManager.ToListWitoutSoder(CouchDataSet);
            var complast = EventManager.ToListWitoutSoder(last);

           bool main= comp.SequenceEqual(complast);
           bool child = EventManager.ToListSoder(CouchDataSet).SequenceEqual(EventManager.ToListSoder(last));
          bool count= CouchDataSet.Soderzhimoe.Count == last.Soderzhimoe.Count;
            return main&&child&&count;
        }
        public List<ImportResultResponse> SetEventDocument(EventCouch CouchDataSet, string user, string id=null)
        {
            List<ImportResultResponse> list = new List<ImportResultResponse>();
            var id1 = "";
            if (id == null)
                id1 = GetUUID();
            else
            {
                id1 = id;
                CouchDataSet._id = id;
                var t=CompareEvent(CouchDataSet,  id);
                if (t)
                    return list;
            }
          var pruf=  SearchEventByNameAndNumber(CouchDataSet.Naimenovanie_izdeliya, CouchDataSet.Oboznachenie);
          if (pruf!=null&&pruf._id != id)
          {

              list.Add(new ImportResultResponse() { id = "000000000000000000000000" });
              return list;
          }
          
              CouchDataSet.Dobavil = user;
            var json = JsonConvert.SerializeObject(CouchDataSet); 
                var request = (HttpWebRequest)WebRequest.Create("http://localhost:5984/events/" + id1);

                ServicePointManager.DefaultConnectionLimit = 1000;

                request.Credentials = new NetworkCredential("admin", "root");
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.KeepAlive = false;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    if (id == null)
                    {
                        var o = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
                        o.Property("_rev").Remove();
                        o.Property("_id").Remove();
                        o.Add("_id", id1); 
                        var json1 = JsonConvert.SerializeObject(o);
                        streamWriter.Write(json1);
                    }
                    else
                    {
                        var o = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);  
                        var json1 = JsonConvert.SerializeObject(o);
                        streamWriter.Write(json1);
                    }
                }
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();

                    var response = JsonConvert.DeserializeObject<ResponseCouch>(responseText);
                    if (response.ok == null)
                        list.Add(
                            new ImportResultResponse()
                            {
                                result = false,
                                id=id1,
                                number_pack = CouchDataSet.Nomer_upakovki,
                                name = CouchDataSet.Naimenovanie_izdeliya,
                                Content =CouchDataSet.Soderzhimoe!=null? CouchDataSet.Soderzhimoe.Select(x => x.Naimenovanie_sostavnoj_edinicy).ToList():null
                            });
                    else
                        list.Add(
                            new ImportResultResponse()
                            {
                                result = true,
                                id = id1,
                                number_pack = CouchDataSet.Nomer_upakovki,
                                name = CouchDataSet.Naimenovanie_izdeliya,
                                Content = CouchDataSet.Soderzhimoe != null ? CouchDataSet.Soderzhimoe.Select(x => x.Naimenovanie_sostavnoj_edinicy).ToList() : null
                            });
                }
           
            return list;
        }
        public List<ImportResultResponse> SetEventDocuments(List<EventCouch> CouchDataSet, string user)
        {
            List<ImportResultResponse> list = new List<ImportResultResponse>();
            
            foreach (var e in CouchDataSet)
            {
               
                    e.Dobavil = user;
                var json = JsonConvert.SerializeObject(e);
                var id = GetUUID();
                var pruf = SearchEventByNameAndNumber(e.Naimenovanie_izdeliya,e.Zavodskoj_nomer);
                if (pruf != null && pruf._id != id)
                {

                    list.Add(
                     new ImportResultResponse()
                     {
                         result = false,
                         number_pack = e.Nomer_upakovki,
                         name = e.Naimenovanie_izdeliya,
                         Content = e.Soderzhimoe.Select(x => x.Naimenovanie_sostavnoj_edinicy).ToList()
                     });
                    continue;
                }
                var request = (HttpWebRequest)WebRequest.Create("http://localhost:5984/events/" + id);

                ServicePointManager.DefaultConnectionLimit = 1000;

                request.Credentials = new NetworkCredential("admin", "root");
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.KeepAlive = false;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var o = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
                    o.Property("_rev").Remove();
                    o.Property("_id").Remove();
                    o.Add("_id", id); 
                    var json1 = JsonConvert.SerializeObject(o);
                    streamWriter.Write(json1);
                }
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();

                    var response = JsonConvert.DeserializeObject<ResponseCouch>(responseText);
                    if (response.ok == null)
                    list.Add(
                        new   ImportResultResponse(){ result=false,
                                                      number_pack = e.Nomer_upakovki,
                                                      name = e.Naimenovanie_izdeliya,
                                                      Content = e.Soderzhimoe.Select(x => x.Naimenovanie_sostavnoj_edinicy).ToList() 
                        });
                           else
                        list.Add(
                            new ImportResultResponse()
                            {
                                result = true,
                                number_pack = e.Nomer_upakovki,
                                name = e.Naimenovanie_izdeliya,
                                Content = e.Soderzhimoe.Select(x => x.Naimenovanie_sostavnoj_edinicy).ToList()
                            });
                }
        }
            return list;
        }
        public  EventCouch  GetEventDocument(string id)
        {  
            var url = "http://localhost:5984/_fti/local/events/_design/getdocuments/by_index?q=_id:" + id;
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();

            var ev = new EventCouch();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<LuceneRequest<EventWar>>(res);

                if (lucene.rows.Count>0)
                {
                  
                    var sod = lucene.rows.First().fields.Содержимое;
                    var sub = JsonConvert.DeserializeObject<List<SubEvent>>(sod);
                     ev = EventManager.ConvertEventWarToEventCouchParent(lucene.rows.First().fields);
                     ev._id = lucene.rows.First().id;
                    ev.Soderzhimoe = sub;
                }
             
                 
            }
            return ev;
        }
        public CouchRequest<EventCouch> GetFilterSortDocuments(int page = 1, int limit = 10, bool archive = false, FilterSort FS=null)
        {
            CouchRequest<EventCouch> list = new CouchRequest<EventCouch>();
            var skip = (page - 1) * limit;
            var q = "";
            var sort="";
             
            
                foreach (var qq in FS.Filters)
                {if(qq.value!="")
                    q += qq.name.Replace(" ","_") + ":" + qq.value+"*^1 AND ";
                }
             
            q += "archive:" + archive.ToString().ToLower();
          int sq=0;
          if (FS.Sorts.Count > 0 && FS.Sorts[0].name!="Дата приёма"&&  FS.Sorts[0].name!="Дата выдачи") sort = "&sort=";
            var qs=FS.Sorts[0];
            if (qs.name == "Номер упаковки" || qs.name == "Количество")
                   {
                       if (qs.value == "1")
                           sort += "/" + qs.name.Replace(" ", "_") + "<int>"; 
                       else
                           sort += "\\" + qs.name.Replace(" ", "_") + "<int>"; 
                   }
               if (qs.name == "Наименование изделия" || qs.name == "Заводской номер" || qs.name == "Местонахождение на складе" || qs.name == "Система")
               {
                   if (qs.value == "1")
                       sort += "/" + qs.name.Replace(" ", "_");
                   else
                       sort += "\\" + qs.name.Replace(" ", "_");
               }
                
           
            var url = "http://localhost:5984/_fti/local/events/_design/searchdocuments/by_fields?q=" + q + sort+"&skip=" + skip + "&limit=" + limit;
             var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();

            var user = new User();
            var lucene1 = new LuceneRequest<EventCouch>();
            lucene1.rows = new List<Row<EventCouch>>();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<LuceneRequest<EventWar>>(res);

                foreach (var l in lucene.rows)
                {
                    var sod = l.fields.Содержимое;
                    var sub = JsonConvert.DeserializeObject<List<SubEvent>>(sod);
                    EventCouch ev = new EventCouch();
                    ev = EventManager.ConvertEventWarToEventCouchParent(l.fields);
                    ev.Soderzhimoe = sub;

                    lucene1.rows.Add(new Row<EventCouch>() { id = l.id, fields = ev });
                }


                var couch = new CouchRequest<EventCouch>();
                couch.total_rows = lucene.total_rows;
                couch.offset = lucene.skip;
                
                couch.rows = new List<RowCouch<EventCouch>>();
                foreach (var r in lucene1.rows)
                {
                    couch.rows.Add(new RowCouch<EventCouch>() { id = r.id, key = r.id, value = r.fields });
                }
                list = couch;
            }
            return list;

        }
      
        public CouchRequestMultiKey<EventCouch> CompareResultFilter(CouchRequestMultiKey<EventCouch> ByDate, CouchRequestMultiKey<EventCouch> ByOter)
        {
            if (ByOter == null || ByOter.rows == null || ByOter.rows.Count== 0)
                return ByDate;
            if (ByDate != null && ByDate.rows != null && ByDate.rows.Count > 0)
            {
                CouchRequestMultiKey<EventCouch> list = new CouchRequestMultiKey<EventCouch>();
                var buf = new List<RowCouchMultiKey<EventCouch>>();
                foreach (var f in ByOter.rows)
                {
                    if (!ByDate.rows.Exists(x => x.id == f.id))
                    {
                        int index = ByOter.rows.FindIndex(a => a.id == f.id);
                        buf.Add(ByOter.rows.ElementAt(index));
                    }
                }
                foreach(var t in buf)
                {
                    ByOter.rows.Remove(t);
                }

                return ByOter;
            }
            else
                return ByOter;
        }

        public CouchRequest<EventCouch> CompareResultFilter(CouchRequestMultiKey<EventCouch> ByDate, CouchRequest<EventCouch> ByOter)
        {
            if (ByDate!=null&&ByDate.rows != null&& ByDate.rows.Count==0)
            {
                CouchRequest<EventCouch> list = new CouchRequest<EventCouch>();
                var buf = new List<RowCouch<EventCouch>>(); 
                foreach (var f in ByOter.rows)
                {
                    if (!ByDate.rows.Exists(x => x.id == f.id))
                    {
                        int index = ByOter.rows.FindIndex(a => a.id == f.id);
                        buf.Add(ByOter.rows.ElementAt(index));
                    }
                }
                foreach (var t in buf)
                {
                    ByOter.rows.Remove(t);
                }

                return ByOter;
            }
            else
                return ByOter;
        }
        //true - datepr; false- datevd;
 
        public CouchRequestMultiKey<EventCouch> OrderByDateDocuments(bool flag, int page = 1, int limit = 10, bool archive = false )
        {
            CouchRequestMultiKey<EventCouch> list = new CouchRequestMultiKey<EventCouch>();
            var skip = (page - 1) * limit;
           
            var url = "http://localhost:5984/events/_design/bydate/_view/bydatepr?startkey=[" + "\"0001-01-01\"," + archive.ToString().ToLower() + "]&endkey=[" + "\"0001-01-01\"," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;
            if (!flag)
                url = "http://localhost:5984/events/_design/bydate/_view/bydatevd?startkey=[" + "\"0001-01-01\"," + archive.ToString().ToLower() + "]&endkey=[" + "\"0001-01-01\"," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();
            var user = new User();

            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<CouchRequestMultiKey<EventCouch>>(res);

            }
            return list;
        }
        public CouchRequest<EventCouch> OrderByDateDocumentsCR(bool flag, int page = 1, int limit = 10, bool archive = false)
        {
            CouchRequest<EventCouch> list = new CouchRequest<EventCouch>();
            var skip = (page - 1) * limit;

            var url = "http://localhost:5984/events/_design/bydate/_view/bydatepr?startkey=[" + "\"0001-01-01\"," + archive.ToString().ToLower() + "]&endkey=[" + "{}," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;
            if (!flag)
                url = "http://localhost:5984/events/_design/bydate/_view/bydatevd?startkey=[" + "\"0001-01-01\"," + archive.ToString().ToLower() + "]&endkey=[" + "{}," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();
            var user = new User();
            var lucene1 = new LuceneRequest<EventCouch>();
            lucene1.rows = new List<Row<EventCouch>>();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<CouchRequestMultiKey<EventCouch>>(res);

                foreach (var l in lucene.rows)
                {

                    lucene1.rows.Add(new Row<EventCouch>() { id = l.id, fields = l.value });
                }


                var couch = new CouchRequest<EventCouch>();
                couch.total_rows = lucene.total_rows;
                couch.offset = lucene.offset;

                couch.rows = new List<RowCouch<EventCouch>>();
                foreach (var r in lucene1.rows)
                {
                    couch.rows.Add(new RowCouch<EventCouch>() { id = r.id, key = r.id, value = r.fields });
                }
                list = couch;





            }
            return list;
        }
        //true - datepr; false- datevd;
        public CouchRequestMultiKey<EventCouch> FilterByDateDocuments(bool flag,int page = 1, int limit = 10, bool archive = false, string startkey = "", string endkey = "")
        {
            CouchRequestMultiKey<EventCouch> list = new CouchRequestMultiKey<EventCouch>();
            var skip = (page - 1) * limit;
            if (startkey == "")
            {
                startkey = "" + DateTime.Today.Date.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day;
            }
            if (endkey == "")
            {
                endkey = "" + DateTime.Today.Date.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day;
            }
           
            var url = "http://localhost:5984/events/_design/bydate/_view/bydatepr?startkey=[" + "\"" + startkey + "\"," + archive.ToString().ToLower() + "]&endkey=[" + "\"" + endkey + "\"," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;
            if(!flag)
                url = "http://localhost:5984/events/_design/bydate/_view/bydatevd?startkey=[" + "\"" + startkey + "\"," + archive.ToString().ToLower() + "]&endkey=[" + "\"" + endkey + "\"," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;
          
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();
            var user = new User();
         
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<CouchRequestMultiKey<EventCouch>>(res);
 
            }
            return list;
        }
        //true - datepr; false- datevd;
        public CouchRequest<EventCouch> FilterByDateDocumentsCR(bool flag, int page = 1, int limit = 10, bool archive = false, string startkey = "", string endkey = "")
        {
            CouchRequest<EventCouch> list = new CouchRequest<EventCouch>();
            var skip = (page - 1) * limit;
            if (startkey == "")
            {
                startkey = "" + DateTime.Today.Date.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day;
            }
            if (endkey == "")
            {
                endkey = "" + DateTime.Today.Date.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day;
            }

            var url = "http://localhost:5984/events/_design/bydate/_view/bydatepr?startkey=[" + "\"" + startkey + "\"," + archive.ToString().ToLower() + "]&endkey=[" + "\"" + endkey + "\"," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;
            if (!flag)
                url = "http://localhost:5984/events/_design/bydate/_view/bydatevd?startkey=[" + "\"" + startkey + "\"," + archive.ToString().ToLower() + "]&endkey=[" + "\"" + endkey + "\"," + archive.ToString().ToLower() + "]" + "&skip=" + skip + "&limit=" + limit;

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();
            var user = new User();
            var lucene1 = new LuceneRequest<EventCouch>();
            lucene1.rows = new List<Row<EventCouch>>();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<CouchRequestMultiKey<EventCouch>>(res);

                foreach (var l in lucene.rows)
                {
     
                    lucene1.rows.Add(new Row<EventCouch>() { id = l.id, fields = l.value });
                }


                var couch = new CouchRequest<EventCouch>();
                couch.total_rows = lucene.total_rows;
                couch.offset = lucene.offset;

                couch.rows = new List<RowCouch<EventCouch>>();
                foreach (var r in lucene1.rows)
                {
                    couch.rows.Add(new RowCouch<EventCouch>() { id = r.id, key = r.id, value = r.fields });
                }
                list = couch;





            }
            return list;
        }
       
        public CouchRequest<EventCouch> GetEventPaginDocuments(int page = 1, int limit = 10, bool archive= false)
        {
            CouchRequest<EventCouch> list = new CouchRequest<EventCouch>();
            var skip = (page - 1) * limit;
            var url = "http://localhost:5984/_fti/local/events/_design/getdocuments/by_archive?q=archive:"+archive+ "&skip="+skip + "&limit=" + limit;
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();

            var user = new User();
            var lucene1 = new LuceneRequest<EventCouch>();
            lucene1.rows = new List<Row<EventCouch>>();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<LuceneRequest<EventWar>>(res);
             
                 foreach(var l in lucene.rows ){
                      var sod = l.fields.Содержимое;
                    var sub = JsonConvert.DeserializeObject<List<SubEvent>>(sod);
                    EventCouch ev = new EventCouch();
                    ev = EventManager.ConvertEventWarToEventCouchParent(l.fields);
                    ev.Soderzhimoe = sub;
                   
                    lucene1.rows.Add(new Row<EventCouch>() { id=l.id,fields=ev});
                 }
                   
                
              var couch = new CouchRequest<EventCouch>();
              couch.total_rows = lucene.total_rows;
              couch.offset = lucene.skip;
              couch.rows = new List<RowCouch<EventCouch>>();
                foreach(var r in lucene1.rows){
                    couch.rows.Add(new RowCouch<EventCouch>() { id=r.id, key=r.id, value=r.fields});
                }
                list = couch;
            }
            return list;

        }
        public object Clone()
        {
            return new WarehouseRequestsRepository();
        }
        public bool SendEventToArchive()
        {
            return true;
        }
        public List<RevsInfo> GetRevisionListEvent(string id)
        {
            var list= new List<RevsInfo>();
            var url = "http://localhost:5984/events/"+id+"?revs_info=true";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<EventCouch>(res);

                list = lucene._revs_info;
            }
            int maxidx = 0;
            int max = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].status == "available")
                {
                    var idx = list[i].rev.IndexOf("-");
                    var substr = list[i].rev.Substring(0,idx);
                    if(Convert.ToInt32(substr)>max)
                    {
                        max = Convert.ToInt32(substr);
                        maxidx = i;
                    }
                }
            }
            list.RemoveAt(maxidx);
                return list;
        }
        public CouchRequest<EventCouch> GetRevisionFiesldsEvent(string id, List<RevsInfo> revs)
        {
            var list = new CouchRequest<EventCouch>();
            var list1= new List<EventCouch>();
           var param="";
            foreach(var r in revs){
                if (r.status == "available")
                {
                    param = "\"" + r.rev + "\"";
                    var url = "http://localhost:5984/events/" + id + "?open_revs=[" + param + "]";
                    var request = (HttpWebRequest)WebRequest.Create(url);

                    request.Credentials = new NetworkCredential("admin", "root");
                    var response = request.GetResponse();
                    using (var responseStream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(responseStream, Encoding.UTF8);
                        var res = reader.ReadToEnd();
                        var str1 = res.IndexOf("{");
                        var str2 = res.LastIndexOf("}");
                        var substr = res.Substring(str1,str2-str1+1);
                        var lucene = JsonConvert.DeserializeObject<EventCouch>(substr);

                        list1.Add(lucene);

                    }
                }
            }
            var couch = new CouchRequest<EventCouch>();
            couch.total_rows =0;
            couch.offset = 0;
            couch.rows = new List<RowCouch<EventCouch>>();
            foreach (var r in list1)
            {
                couch.rows.Add(new RowCouch<EventCouch>() { id = id, key = id, value = r });
            }
            list = couch;
           
            return list;
        }
        public EventCouch SearchEventByNameAndNumber(string name, string number, Boolean? archive = null)
        {

            var url = "http://localhost:5984/_fti/local/events/_design/searchdocuments/by_name_number?q=Наименование_изделия:" + "\"" + name + "\"&Заводской_номер:\"" + number + ((archive == null) ? ("\"&archive=" + archive) : "") + "^10";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();

            var user = new EventCouch();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<LuceneRequest<EventCouch>>(res);

                if (lucene.rows.Count <= 0)
                {
                    return null;
                } 
                if (lucene.rows.First().fields.Naimenovanie_izdeliya == name && lucene.rows.First().fields.Zavodskoj_nomer == number)
                {
                    user = lucene.rows.First().fields;
                    user._id = lucene.rows.First().id;

                }
                else return null;
               
            }

            return  user;
        }
        public void Dispose()
        {

        }
    }
}
