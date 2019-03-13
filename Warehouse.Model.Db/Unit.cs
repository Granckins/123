using Newtonsoft.Json;
using System;
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
        [JsonProperty("child_id")]
        public string child_id { get; set; }
        [JsonProperty("parent_id")]
        public string parent_id { get; set; }
        [JsonProperty("type")]
        public int type { get; set; }
        [JsonProperty("Номер_упаковки")]
        public int Nomer_upakovki { get; set; }
        [JsonProperty("Наименование_изделия")]
        public string Naimenovanie_izdeliya { get; set; }
        [JsonProperty("Заводской_номер")]
        public string Zavodskoj_nomer { get; set; }
        [JsonProperty("Количество")]
        public int Kolichestvo { get; set; }
        [JsonProperty("Обозначение")]
        public string Oboznachenie { get; set; }
        [JsonProperty("Содержимое")]
        public List<SubEvent> Soderzhimoe { get; set; }
        [JsonProperty("Система")]
        public string Sistema { get; set; }
        [JsonProperty("Принадлежность")]
        public string Prinadlezhnost { get; set; }

        [JsonProperty("Стоимость")]
        public float Stoimost { get; set; }
        [JsonProperty("Ответственный")]
        public string Otvetstvennyj { get; set; }
        [JsonProperty("Местонахождение_на_складе")]
        public string Mestonahozhdenie_na_sklade { get; set; }
        [JsonProperty("Вес_брутто")]
        public float Ves_brutto { get; set; }
        [JsonProperty("Вес_нетто")]
        public float Ves_netto { get; set; }
        [JsonProperty("Длина")]
        public float Dlina { get; set; }
        [JsonProperty("Ширина")]
        public float Shirina { get; set; }
        [JsonProperty("Высота")]
        public float Vysota { get; set; }

        [JsonProperty("Дата_приёма")]
        public DateTime? Data_priyoma { get; set; }
        [JsonProperty("Откуда")]
        public string Otkuda { get; set; }
        [JsonProperty("Дата_выдачи")]
        public DateTime? Data_vydachi { get; set; }
        [JsonProperty("Куда")]
        public string Kuda { get; set; }
        [JsonProperty("Номер_пломбы")]
        public string Nomer_plomby { get; set; }
        [JsonProperty("Примечание")]
        public string Primechanie { get; set; }
        [JsonProperty("Добавил")]
        public string Dobavil { get; set; }
        [JsonProperty("Дата_изменения")]
        public DateTime Data_ismenen { get; set; }

        public List<RevsInfo> _revs_info { get; set; }
        public List<Attachment> attachments { get; set; }
        public Unit()
        {
            attachments = new List<Attachment>();
            _revs_info = new List<RevsInfo>();
            archive = false;
        }

        public void AddAttachment(string str)
        {
            str = "{" + str + "}";
            var lucene = JsonConvert.DeserializeObject<Attachment>(str);
            attachments.Add(lucene);
        }
    }
}
