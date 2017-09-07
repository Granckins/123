using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    

    public class Row<Obj>
    {
        public double score { get; set; }
        public string id { get; set; }
        public Obj fields { get; set; }
    }

    public class LuceneRequest<Obj>
    {
        public string q { get; set; }
        public int fetch_duration { get; set; }
        public int total_rows { get; set; }
        public int limit { get; set; }
        public int search_duration { get; set; }
        public string etag { get; set; }
        public int skip { get; set; }
        public List<Row<Obj>> rows { get; set; }
    }
}
