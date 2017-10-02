using Newtonsoft.Json;
using RedBranch.Hammock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class SubEvent  {
        [JsonProperty("Наименование_составной_единицы")]
        public string Naimenovanie_sostavnoj_edinicy { get; set; }
        [JsonProperty("Обозначение_составной_единицы")]
        public string Oboznachenie_sostavnoj_edinicy { get; set; }
        [JsonProperty("Количество_составных_единиц")]
        public int Kolichestvo_sostavnyh_edinic { get; set; }
         public string ToStringNew(){
             return Naimenovanie_sostavnoj_edinicy == null ? "" : Naimenovanie_sostavnoj_edinicy + " " + Oboznachenie_sostavnoj_edinicy == null ? "" : Oboznachenie_sostavnoj_edinicy
                 + " " + Kolichestvo_sostavnyh_edinic == null ? "" : Kolichestvo_sostavnyh_edinic.ToString();
         }

    }
}
