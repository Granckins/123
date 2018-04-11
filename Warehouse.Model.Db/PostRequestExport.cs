using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class PostRequestExport
    {

        [JsonProperty("dateiz")]
        public string dateiz { get; set; }
        [JsonProperty("all_events")]
        public bool all_events { get; set; }
    }
}
