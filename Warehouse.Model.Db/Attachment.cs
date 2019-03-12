using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
   public class Attachment
   {
       [JsonProperty("content_type")]
        public string content_type { get; set; }
           [JsonProperty("revpos")]
        public int revpos { get; set; }
           [JsonProperty("digest")]
        public string digest { get; set; }
           [JsonProperty("length")]
        public int length { get; set; }
           [JsonProperty("stub")]
        public bool stub { get; set; }
        [JsonProperty("name")]
           public string name { get; set; }
    }
}
