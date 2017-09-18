using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class ResponseCouch
    {
        public bool ok { get; set; }
        public string id { get; set; }
        public string rev { get; set; }
        public string error { get; set; }
        public string reason { get; set; }
    }
}
