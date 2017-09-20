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
        [JsonProperty("Принадлежность_к_объекту")]
        public string Prinadlezhnost_k_obektu { get; set; }
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
        public DateTime Data_priyoma { get; set; }
        [JsonProperty("Откуда")]
        public string Otkuda { get; set; }
        [JsonProperty("Дата_выдачи")]
        public DateTime Data_vydachi { get; set; }
        [JsonProperty("Куда")]
        public string Kuda { get; set; }
        [JsonProperty("Номер_пломбы")]
        public string Nomer_plomby { get; set; }
        [JsonProperty("Примечание")]

        public string Primechanie { get; set; }
 
      
     
     
    }
}
