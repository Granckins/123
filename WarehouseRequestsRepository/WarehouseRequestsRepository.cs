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
        public object Clone()
        {
            return new WarehouseRequestsRepository();
        }

        public void Dispose()
        {

        }
    }
}
