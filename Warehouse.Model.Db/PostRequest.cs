using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class PostRequest<Obj>
    {
        [JsonProperty("page")]
    public  int page { get; set; }
        [JsonProperty("filter")]
        public string filter { get; set; }
        [JsonProperty("sort")]
        public string sort { get; set; }
          [JsonProperty("limit")]
    public int limit { get; set; }
          [JsonProperty("archive_str")]
          public bool archive_str { get; set; }
          [JsonProperty("Nomer_upakovki_str")]
    public int Nomer_upakovki_str { get; set; }
          [JsonProperty("Naimenovanie_izdeliya_str")]
          public string Naimenovanie_izdeliya_str { get; set; }
          [JsonProperty("Zavodskoj_nomer_str")]
          public string Zavodskoj_nomer_str { get; set; }
          [JsonProperty("Oboznachenie_str")]
          public string Oboznachenie_str { get; set; }
          [JsonProperty("Soderzhimoe_str")]
          public List<SubEvent> Soderzhimoe_str { get; set; }
          [JsonProperty("Sistema_str")]
          public string Sistema_str { get; set; }
          [JsonProperty("Prinadlezhnost_str")]
          public string Prinadlezhnost_str { get; set; }
          [JsonProperty("Mestonahozhdenie_na_sklade_str")]
          public string Mestonahozhdenie_na_sklade_str { get; set; }
          [JsonProperty("Data_priyoma_str1")]
          public DateTime? Data_priyoma_str1 { get; set; }
          [JsonProperty("Data_vydachi_str1")]
          public DateTime? Data_vydachi_str1 { get; set; }
          [JsonProperty("Data_priyoma_str2")]
          public DateTime? Data_priyoma_str2 { get; set; }
          [JsonProperty("Data_vydachi_str2")]
          public DateTime? Data_vydachi_str2 { get; set; }
          [JsonProperty("Primechanie_str")]
          public string Primechanie_str { get; set; }
    public Obj entity { get; set; }
    }
}
