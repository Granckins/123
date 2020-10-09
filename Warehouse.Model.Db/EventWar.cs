using Newtonsoft.Json;
using RedBranch.Hammock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
   
    public class EventWar
    { 
        [JsonProperty("archive")]
        public bool archive { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("_rev")]
        public string _rev { get; set; }
           [JsonProperty("Номер_упаковки")]
        public int  Номер_упаковки  { get; set; }
          [JsonProperty("Наименование_изделия")]
           public string Наименование_изделия  { get; set; }
            [JsonProperty("Заводской_номер")]
      public string  Заводской_номер { get; set; }
                    [JsonProperty("Количество")]
        public int Количество { get; set; }
            [JsonProperty("Обозначение")]
           public string  Обозначение { get; set; }
            [JsonProperty("Содержимое")]
            public string Содержимое { get; set; }
                    [JsonProperty("Наименование_составной_единицы")]
            public string     Наименование_составной_единицы { get; set; }
                   [JsonProperty("Обозначение_составной_единицы")]
                 public string    Обозначение_составной_единицы { get; set; }
                        [JsonProperty("Количество_составных_единиц")]
                   public int      Количество_составных_единиц { get; set; }
        [JsonProperty("Система")]
                     public string        Система { get; set; }
        [JsonProperty("Проект")]
        public string Проект { get; set; }
        [JsonProperty("Принадлежность")]
                             public string    Принадлежность { get; set; } 
           [JsonProperty("Стоимость")]
                                    public float   Стоимость { get; set; }
                   [JsonProperty("Ответственный")]
                             public string   Ответственный { get; set; }
                    [JsonProperty("Местонахождение_на_складе")]
                              public string      Местонахождение_на_складе { get; set; }
             [JsonProperty("Вес_брутто")]
                                       public float   Вес_брутто { get; set; }
                     [JsonProperty("Вес_нетто")]
                                         public float       Вес_нетто { get; set; }
              [JsonProperty("Длина")]
                                           public float     Длина { get; set; }
         [JsonProperty("Ширина")]
                                              public float      Ширина { get; set; }
                 [JsonProperty("Высота")]
                                               public float     Высота { get; set; }
         [JsonProperty("Номер_контейнера")]
                                               public string      Номер_контейнера { get; set; }
              [JsonProperty("Номер_упаковочного_ящика")]
                                                public string     Номер_упаковочного_ящика { get; set; }
              [JsonProperty("Дата_приёма")]
                                               public DateTime    Дата_приёма { get; set; }
            [JsonProperty("Откуда")]
                                                   public string    Откуда { get; set; }
                    [JsonProperty("Дата_выдачи")]
                                                    public DateTime     Дата_выдачи { get; set; }
           [JsonProperty("Куда")]
                                                     public string     Куда  { get; set; }
          [JsonProperty("Номер_пломбы")]
                                                       public string         Номер_пломбы { get; set; }
            [JsonProperty("Примечание")]
                                                       public string Примечание { get; set; }
            [JsonProperty("Добавил")]
            public string Добавил { get; set; }
            [JsonProperty("Дата_изменения")]
            public DateTime Дата_изменения { get; set; }
            public List<RevsInfo> _revs_info { get; set; }
          public  EventWar()
            {
                this.archive = false;
                _revs_info = new List<RevsInfo>();
            }

    }
}
