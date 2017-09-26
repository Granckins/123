using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Model.Db;

namespace Warehouse.Model
{
 public   class EditSubEvent
    {
    public RowCouch<EventCouch> edit_event {get;set;}
    public int subidx { get; set; }  
    }
}
