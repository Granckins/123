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

            var url = "http://localhost:5984/_fti/local/users/_design/foo/by_username?q=UserName:"+username;
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
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:5984/events/" + id + "?" + CouchDataSet._rev);

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

        public List<ImportResultResponse> SetEventDocument(EventCouch CouchDataSet, string id=null)
        {
            List<ImportResultResponse> list = new List<ImportResultResponse>();
            var id1 = "";
            if (id == null)
                id1 = GetUUID();
            else id1 = id;
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
                        var json1 = JsonConvert.SerializeObject(o);
                        streamWriter.Write(json1);
                    }
                    else
                    {
                        var o = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json); 
                        o.Add("_id", id1); 
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
        public List<ImportResultResponse> SetEventDocuments(List<EventCouch> CouchDataSet)
        {
            List<ImportResultResponse> list = new List<ImportResultResponse>();
            
            foreach (var e in CouchDataSet)
            {
                var json = JsonConvert.SerializeObject(e);
                var id = GetUUID();
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
                    ev.Soderzhimoe = sub;
                }
             
                 
            }
            return ev;
        }
        public  CouchRequest<EventCouch>  GetEventPaginDocuments(int page = 1, int limit = 10)
        {
            CouchRequest<EventCouch>  list = new  CouchRequest<EventCouch> ();
            var skip = (page-1)*limit;
            var url = "http://localhost:5984/events/_design/pagindef/_view/foo?skip="+skip+"&limit="+limit;
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();

            var user = new User();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var couch = JsonConvert.DeserializeObject<CouchRequest<EventCouch>>(res);
                list = couch;
            }
            return list;

        }
        public object Clone()
        {
            return new WarehouseRequestsRepository();
        }

        public void Dispose()
        {

        }
    }
}
