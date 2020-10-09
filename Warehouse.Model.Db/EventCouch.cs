using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("Проект")]
        public string Project { get; set; }
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
        public EventCouch()
        {
            _revs_info = new List<RevsInfo>();
            Soderzhimoe = new List<SubEvent>();
            this.archive = false;
        }
        public string ToString()
        {
            var sod = "";
            var str = "";
            foreach (var s in Soderzhimoe)
            {
                sod += " (Наименование:" + (s.Naimenovanie_sostavnoj_edinicy == null ? "" : s.Naimenovanie_sostavnoj_edinicy).Replace(';', ',')
                    + ", Обозначение:"
                    + (s.Oboznachenie_sostavnoj_edinicy == null ? "" : s.Oboznachenie_sostavnoj_edinicy).Replace(';', ',') + ", Количество:"
                    + s.Kolichestvo_sostavnyh_edinic + ")";
            }


            str = Nomer_upakovki.ToString().Replace(';', ',') + ";" +
             (Naimenovanie_izdeliya == null ? "" : Naimenovanie_izdeliya).Replace(';', ',') + ";" +
          (Zavodskoj_nomer == null ? "" : Zavodskoj_nomer).Replace(';', ',') + ";" +
              Kolichestvo.ToString().Replace(';', ',') + ";" +
                 (Mestonahozhdenie_na_sklade == null ? "" : Mestonahozhdenie_na_sklade).Replace(';', ',') + ";" +
          (Oboznachenie == null ? "" : Oboznachenie).Replace(';', ',') + ";" +
         sod + ";" +
         (Sistema == null ? "" : Sistema).Replace(';', ',') + ";" +
         (Project == null ? "" : Project).Replace(';', ',') + ";" +
             (Otvetstvennyj == null ? "" : Otvetstvennyj).Replace(';', ',') + ";" +
         (Prinadlezhnost == null ? "" : Prinadlezhnost).Replace(';', ',') + ";";
            if (Data_priyoma != null)
                str += (Data_priyoma.Value.Date == new DateTime(1, 1, 1).Date ? "" : Data_priyoma.Value.Day + "." + Data_priyoma.Value.Month + "." + Data_priyoma.Value.Year) + ";";
            else
                str += ";";

            str += (Otkuda == null ? "" : Otkuda).Replace(';', ',') + ";";


            if (Data_vydachi != null)
                str += (Data_vydachi.Value.Date == new DateTime(1, 1, 1).Date ? "" : Data_vydachi.Value.Day + "." + Data_vydachi.Value.Month + "." + Data_vydachi.Value.Year) + ";";
            else
                str += ";";

            str += (Kuda == null ? "" : Kuda).Replace(';', ',') + ";" +
             (Nomer_plomby == null ? "" : Nomer_plomby).Replace(';', ',') + ";" +
               Stoimost.ToString().Replace(';', ',') + ";" +
              Ves_brutto.ToString().Replace(';', ',') + ";" +
           Ves_netto.ToString().Replace(';', ',') + ";" +
              Dlina.ToString().Replace(';', ',') + ";" +
      Shirina.ToString().Replace(';', ',') + ";" +
          Vysota.ToString().Replace(';', ',') + ";" +

              (Primechanie == null ? "" : Primechanie).Replace(';', ',') + ";" +
              (Dobavil == null ? "" : Dobavil).Replace(';', ',') + ";\n";
            return str;
        }


    }
}
