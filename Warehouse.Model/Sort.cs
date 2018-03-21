using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model
{
    public class Sort
    {
        [JsonProperty("name")]
        public string name { get; set; }
        // 0 - not; 1 - 1....15; 2 - 15 ....1;
        [JsonProperty("order")]
        public int order { get; set; }
    }
}
