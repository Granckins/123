using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class Revisions
    {
        public int start { get; set; }
        public List<string> ids { get; set; }
    }

    public class RootRevisions
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public bool _deleted { get; set; }
        public Revisions _revisions { get; set; }
    }
}
