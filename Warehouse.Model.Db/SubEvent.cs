using Newtonsoft.Json;
using RedBranch.Hammock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
    public class SubEvent : Document
    {
        [JsonProperty("Наименование_составной_единицы")]
        public string Наименование_составной_единицы { get; set; }
        [JsonProperty("Обозначение_составной_единицы")]
        public string Обозначение_составной_единицы { get; set; }
        [JsonProperty("Количество_составных_единиц")]
        public int Количество_составных_единиц { get; set; }
    }
}
