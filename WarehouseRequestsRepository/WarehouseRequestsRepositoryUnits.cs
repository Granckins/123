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
using System.Text.RegularExpressions;
namespace Warehouse.Core.Repositories
{
    public class WarehouseRequestsRepositoryUnits : IRepositoryRequestsUnits
    {
        public CouchRequest<Unit> GetUnits()
        {
            CouchRequest<Unit> list = new CouchRequest<Unit>();
            var url = "http://localhost:5984/_fti/local/units/_design/getunits/by_archive?q=archive:" + false;
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Credentials = new NetworkCredential("admin", "root");
            var response = request.GetResponse();

            var user = new User();
            var lucene1 = new LuceneRequest<Unit>();
            lucene1.rows = new List<Row<Unit>>();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var res = reader.ReadToEnd();
                var lucene = JsonConvert.DeserializeObject<LuceneRequest<Unit>>(res);

                foreach (var l in lucene.rows)
                {
                    var str = l.fields._attachments;
                    string pattern = @"^(\{){1}(.*?)(\}){1}$";
                    str = Regex.Replace(str, pattern, "$2");
                    string s = str;

                    Regex regex = new Regex(@"(?<=\{)[^}]*(?=\})", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(str);

                    List<String> st = matches.Cast<Match>().Select(m => m.Value).Distinct().ToList();
                    foreach (var a in st)
                    {
                        l.fields.AddAttachment(a);
                    }

                    Regex regex1 = new Regex("(?<counter>{)(?>(?<counter>{)|(?<-counter>})|[^{}]+)+?(?(counter)(?!))");
                    MatchCollection matches1 = regex1.Matches(s);
                    List<String> st1 = matches1.Cast<Match>().Select(m => m.Value).Distinct().ToList();

                    List<string> name = new List<string>();
                    var dsds = str;
                    foreach (var dd in st1)
                    {
                        dsds = dsds.Replace(dd, "");
                    }
                    regex = new Regex("\"([^\"]*)\"", RegexOptions.IgnoreCase);
                    matches = regex.Matches(dsds);

                    st = matches.Cast<Match>().Select(m => m.Value).Distinct().ToList();
                    foreach (var a in st)
                    {
                        name.Add(a.Replace("\"", ""));

                    }
                    for (int i = 0; i < l.fields.attachments.Count; i++)
                    {
                        l.fields.attachments[i].name = name[i];
                    }
                }


                var couch = new CouchRequest<Unit>();
                couch.total_rows = lucene.total_rows;
                couch.offset = lucene.skip;
                couch.rows = new List<RowCouch<Unit>>();
                foreach (var r in lucene.rows)
                {
                    couch.rows.Add(new RowCouch<Unit>() { id = r.id, key = r.id, value = r.fields });
                }
                list = couch;
            }
            return list;

        }
        public List<RevsInfo> GetRevisionListEvent(string id, bool flag = true)
        {
            var list = new List<RevsInfo>();
            var url = "http://localhost:5984/units/" + id + "?revs_info=true";
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
                if (flag)
                {
                    if (list[i].status == "available")
                    {
                        var idx = list[i].rev.IndexOf("-");
                        var substr = list[i].rev.Substring(0, idx);
                        if (Convert.ToInt32(substr) > max)
                        {
                            max = Convert.ToInt32(substr);
                            maxidx = i;
                        }
                    }
                }
                else
                {

                    var idx = list[i].rev.IndexOf("-");
                    var substr = list[i].rev.Substring(0, idx);
                    if (Convert.ToInt32(substr) > max)
                    {
                        max = Convert.ToInt32(substr);
                        maxidx = i;
                    }

                }
            }
            list.RemoveAt(maxidx);
            return list;
        }
        public void Dispose()
        {

        }
        public object Clone()
        {
            return new WarehouseRequestsRepositoryUnits();
        }
    }
}
