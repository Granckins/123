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
        public Obj value { get; set; }
    }

    public class CouchRequest<Obj>
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public List<RowCouch<Obj>> rows { get; set; }
    }
}
