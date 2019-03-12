using Newtonsoft.Json;
using System.Collections.Generic;

namespace Warehouse.Model.Db
{
    public class Unit
    {
        [JsonProperty("archive")]
        public bool archive { get; set; }
        [JsonProperty("_rev")]
        public string _rev { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("_attachments")]
        public string _attachments { get; set; }
    public List<Attachment> attachments { get; set; }
    public Unit()
    {
        attachments = new List<Attachment>();

    }
        public void AddAttachment (string str)
    {
        str = "{" + str + "}";
            var lucene = JsonConvert.DeserializeObject<Attachment>(str);
            attachments.Add(lucene);
        }
    }
}
