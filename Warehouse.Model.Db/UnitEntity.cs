using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Warehouse.Model.Db
{
    public class UnitEntity
    {
        [JsonProperty("archive")]
        public bool archive { get; set; }
        [JsonProperty("_rev")]
        public string _rev { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }

        [JsonProperty("Ответственный")]
        public string Otvetstvennyj { get; set; }
        [JsonProperty("Местонахождение_на_складе")]
        public string Mestonahozhdenie_na_sklade { get; set; }

        [JsonProperty("Дата_приёма")]
        public DateTime? Data_priyoma { get; set; }
        [JsonProperty("Откуда")]
        public string Otkuda { get; set; }
        [JsonProperty("Дата_выдачи")]
        public DateTime? Data_vydachi { get; set; }
        [JsonProperty("Куда")]
        public string Kuda { get; set; }

        [JsonProperty("Примечание")]
        public string Primechanie { get; set; }
        [JsonProperty("Добавил")]
        public string Dobavil { get; set; }
        [JsonProperty("Дата_изменения")]
        public DateTime Data_ismenen { get; set; }

        public List<RevsInfo> _revs_info { get; set; }
        public UnitEntity()
        {

            _revs_info = new List<RevsInfo>();
            archive = false;
        }
    }
}
