using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class PostRequest<Obj>
    {
    public  int page { get; set; }
    public int limit { get; set; }
    public Obj entity { get; set; }
    }
}
