using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
 

    public class RootObject<Obj>
    {
        public List<RowCount<Obj>> rows { get; set; }
    }
    public class RowCount<Obj>
    {
        public Obj key { get; set; }
        public int value { get; set; }
    }
}
