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
        [JsonProperty("archive")]
        public bool archive { get; set; }
        [JsonProperty("_rev")]
        public string _rev { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }
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
        public string Dobavil{ get; set; }
        [JsonProperty("Дата_изменения")]
        public DateTime Data_ismenen { get; set; }
        public List<RevsInfo> _revs_info { get; set; }
       public EventCouch()
        {
            _revs_info = new List<RevsInfo>();
            Soderzhimoe = new List<SubEvent>();
            this.archive = false;
        }
       public string ToString()
       {
           var sod="";
           var str="";
                   foreach(var s in Soderzhimoe){
                       sod += "( " + s.Kolichestvo_sostavnyh_edinic + "," + (s.Naimenovanie_sostavnoj_edinicy == null ? "" : s.Naimenovanie_sostavnoj_edinicy).Replace(';', ',') + ","
                           + (s.Oboznachenie_sostavnoj_edinicy == null ? "" : s.Oboznachenie_sostavnoj_edinicy).Replace(';', ',') + ")\n";
           }
             

                   str = Nomer_upakovki.ToString().Replace(';', ',') + ";" +
                    (Naimenovanie_izdeliya == null ? "" : Naimenovanie_izdeliya).Replace(';', ',') + ";" +
                 (Zavodskoj_nomer == null ? "" : Zavodskoj_nomer).Replace(';', ',') + ";" +
                     Kolichestvo.ToString().Replace(';', ',') + ";" +
                 (Oboznachenie == null ? "" : Oboznachenie).Replace(';', ',') + ";" +
                sod + ";" +
                (Sistema == null ? "" : Sistema).Replace(';', ',') + ";" +
                (Prinadlezhnost == null ? "" : Prinadlezhnost).Replace(';', ',') + ";" +
                   Stoimost.ToString().Replace(';', ',') + ";" +
                  (Otvetstvennyj == null ? "" : Otvetstvennyj).Replace(';', ',') + ";" +
                   (Mestonahozhdenie_na_sklade == null ? "" : Mestonahozhdenie_na_sklade).Replace(';', ',') + ";" +
                     Ves_brutto.ToString().Replace(';', ',') + ";" +
                  Ves_netto.ToString().Replace(';', ',') + ";" +
                     Dlina.ToString().Replace(';', ',') + ";" +
             Shirina.ToString().Replace(';', ',') + ";" +
                 Vysota.ToString().Replace(';', ',') + ";";

                   if (Data_priyoma != null)
                       str += (Data_priyoma.Value.Date == new DateTime(1, 1, 1).Date ? "" : Data_priyoma.ToString()) + ";";
                   else
                       str +=   ";";

                   str += (Otkuda == null ? "" : Otkuda).Replace(';', ',') + ";";

                   if (Data_vydachi != null)
                       str += (Data_vydachi.Value.Date == new DateTime(1, 1, 1).Date ? "" : Data_vydachi.ToString()) + ";";
                   else
                       str += ";";

                   str += (Kuda == null ? "" : Kuda).Replace(';', ',') + ";" +
                     (Nomer_plomby == null ? "" : Nomer_plomby).Replace(';', ',') + ";" +                   (Primechanie == null ? "" : Primechanie).Replace(';', ',') + ";" +              (Dobavil == null ? "" : Dobavil).Replace(';', ',') + ";";    
          return str;
       }
     
     
    }
}
