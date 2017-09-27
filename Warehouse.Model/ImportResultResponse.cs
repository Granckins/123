using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model
{
  public  class ImportResultResponse
    {
      public bool result { get; set; }
      public int number_pack { get; set; }
      public string id { get; set; }
     public string name { get; set; }
     public List<string> Content { get; set; }
    }
}
