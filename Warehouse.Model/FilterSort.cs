using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model
{
   public class FilterSort
    {
       public List<Filter> Filters {get;set;}
       public List<Sort> Sorts { get; set; }
       public FilterSort()
       {
           Filters = new List<Filter>();
           Sorts = new List<Sort>();
       }
       public void FromStringToObject(string filtersname,string filtersvalue, string sortsname, string sortsvalue){
           if(filtersname!=""&&filtersvalue!=""){
            var FiltersName = filtersname.Split(';').ToList();
            var FiltersValue = filtersvalue.Split(';').ToList();
               for(int i=0;i< FiltersName.Count;i++)
               {
                   Filters.Add(new Filter() { name = FiltersName[i], value = FiltersValue[i] });
               }
           }
           if (sortsname != "" && sortsvalue != "")
           {
               var SortsName = filtersname.Split(';').ToList();
               var SortsValue = filtersvalue.Split(';').ToList();
               for (int i = 0; i < SortsName.Count; i++)
               {
                   Sorts.Add(new Sort() { name = SortsName[i], value = SortsValue[i] });
               }
           }
       }
    }
   public class Filter
   {
       public string name;
       public string value;
   }
   public class Sort
   {
       public string name;
       //0 - not; 1-1..15; 2-15...1;
       public string value;
   }

}
