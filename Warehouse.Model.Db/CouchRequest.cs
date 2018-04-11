using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    

     public class RowCouch<Obj>
    {
        public string id { get; set; }
        public string key { get; set; }
        public int page { get; set; }  

        public string filtername { get; set; }
        public string filtervalue { get; set; } 
        public string sortname { get; set; } 
        public string sortvalue { get; set; } 
        public string datepr { get; set; } 
        public string datevd { get; set; }

        public int limit { get; set; } 
        public bool archive_str { get; set; }
        public Obj value { get; set; }

    }
     
    public class CouchRequest<Obj>
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public List<RowCouch<Obj>> rows { get; set; }
        public List<Obj> ToEventCouch()
        {
            var list = new List<Obj>();
            foreach (var r in this.rows)
            {
                list.Add(r.value);
            }
            return list;
        }
    }
    public class CouchRequestMultiKey<Obj>
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public List<RowCouchMultiKey<Obj>> rows { get; set; }
    }
    public class RowCouchMultiKey<Obj>
    {
        public string id { get; set; }
        public List<string> key { get; set; }
      
        public Obj value { get; set; }
    }
     
}
