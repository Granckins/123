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
        public List<ImportResultResponse> SetDocument(List<EventCouch> CouchDataSet)
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


                    streamWriter.Write(json);
                }
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();

                    var response = JsonConvert.DeserializeObject<ResponseCouch>(responseText);
                    if (response.ok == null)
                    list.Add(
                        new   ImportResultResponse(){ result=false,
                                                      number_pack = e.Номер_упаковки,
                                                      name = e.Наименование_изделия,
                                                      Content = e.Содержимое.Select(x => x.Наименование_составной_единицы).ToList() 
                        });
                           else
                        list.Add(
                            new ImportResultResponse()
                            {
                                result = true,
                                number_pack = e.Номер_упаковки,
                                name = e.Наименование_изделия,
                                Content = e.Содержимое.Select(x => x.Наименование_составной_единицы).ToList()
                            });
                }
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
