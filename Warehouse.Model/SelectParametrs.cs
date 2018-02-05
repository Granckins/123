using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model
{
  public  class SelectParametrs
  {
      [JsonProperty("Filters")]
      public string Filters;
      [JsonProperty("ss")]
      public string ss;
         [JsonProperty("Sorts")]
      public string Sorts;
         public SelectParametrs() 
    {
        

    }
    }
}
