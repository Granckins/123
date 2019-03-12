using Newtonsoft.Json; 


namespace Warehouse.Model.Db
{
    public class Place
    { 
        [JsonProperty("_rev")]
        public string _rev { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("Местонахождение_на_складе")]
        public string Mestonahozhdenie_na_sklade { get; set; }
    }
}