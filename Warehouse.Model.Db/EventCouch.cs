using Newtonsoft.Json;
using RedBranch.Hammock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class EventCouch  
    {
        [JsonProperty("Номер_упаковки")]
        public int Номер_упаковки { get; set; }
        [JsonProperty("Наименование_изделия")]
        public string Наименование_изделия { get; set; }
        [JsonProperty("Заводской_номер")]
        public string Заводской_номер { get; set; }
        [JsonProperty("Количество")]
        public int Количество { get; set; }
        [JsonProperty("Обозначение")]
        public string Обозначение { get; set; }
        [JsonProperty("Содержимое")]
        public List<SubEvent> Содержимое { get; set; }
        [JsonProperty("Система")]
        public string Система { get; set; }
        [JsonProperty("Принадлежность")]
        public string Принадлежность { get; set; }
        [JsonProperty("Принадлежность_к_объекту")]
        public string Принадлежность_к_объекту { get; set; }
        [JsonProperty("Стоимость")]
        public float Стоимость { get; set; }
        [JsonProperty("Ответственный")]
        public string Ответственный { get; set; }
        [JsonProperty("Местонахождение_на_складе")]
        public string Местонахождение_на_складе { get; set; }
        [JsonProperty("Вес_брутто")]
        public float Вес_брутто { get; set; }
        [JsonProperty("Вес_нетто")]
        public float Вес_нетто { get; set; }
        [JsonProperty("Длина")]
        public float Длина { get; set; }
        [JsonProperty("Ширина")]
        public float Ширина { get; set; }
        [JsonProperty("Высота")]
        public float Высота { get; set; }
        [JsonProperty("Номер_контейнера")]
        public string Номер_контейнера { get; set; }
        [JsonProperty("Номер_упаковочного_ящика")]
        public string Номер_упаковочного_ящика { get; set; }
        [JsonProperty("Дата_приёма")]
        public DateTime Дата_приёма { get; set; }
        [JsonProperty("Откуда")]
        public string Откуда { get; set; }
        [JsonProperty("Дата_выдачи")]
        public DateTime Дата_выдачи { get; set; }
        [JsonProperty("Куда")]
        public string Куда { get; set; }
        [JsonProperty("Номер_пломбы")]
        public string Номер_пломбы { get; set; }
        [JsonProperty("Примечание")]

        public string Примечание { get; set; }

      
    }
}
