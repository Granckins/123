using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db
{
  public static class EventManager
    {
      public static EventCouch ConvertEventWarListToEventCouch(List<EventWar> Events)
        {
            EventCouch EC = new EventCouch();
            var valid = true;
            if (Events[0].Наименование_составной_единицы != "")
                valid = false;
            int i = 0;
            if (valid)
            {
                foreach (var e in Events)
                {
                    if (i == 0)
                    {
                        EC = ConvertEventWarToEventCouchParent(e);
                    }
                    else
                    {
                        if (e.Наименование_составной_единицы != "")
                        {
                            EC.Soderzhimoe.Add(ConvertEventWarToSubEvent(e));
                        }

                    }

                    i++;
                }
            }

            return EC;
        }
      public static List<string> ToListWitoutSoder(EventCouch EC)
      {
          var list = new List<string>();
         list.Add(EC.archive.ToString());
           list.Add( EC.Nomer_upakovki.ToString());
           list.Add(EC.Naimenovanie_izdeliya == "" ? null : EC.Naimenovanie_izdeliya);
           list.Add( EC.Zavodskoj_nomer== "" ? null :  EC.Zavodskoj_nomer);
           list.Add( EC.Kolichestvo.ToString() );
           list.Add( EC.Oboznachenie == "" ? null :EC.Oboznachenie );
         
           list.Add( EC.Sistema== "" ? null :EC.Sistema );
           list.Add( EC.Prinadlezhnost == "" ? null :EC.Prinadlezhnost  );
            list.Add( EC.Stoimost.ToString());
           list.Add( EC.Otvetstvennyj== "" ? null : EC.Otvetstvennyj );
           list.Add( EC.Mestonahozhdenie_na_sklade== "" ? null : EC.Mestonahozhdenie_na_sklade);
           list.Add( EC.Ves_brutto.ToString());
          list.Add(  EC.Ves_netto.ToString());
          list.Add(  EC.Dlina.ToString() );
          list.Add(  EC.Shirina.ToString());
           list.Add( EC.Vysota.ToString() );
           list.Add( EC.Data_priyoma.ToString());
           list.Add( EC.Otkuda== "" ? null :EC.Otkuda );
           list.Add( EC.Data_vydachi.ToString() );
           list.Add( EC.Kuda == "" ? null : EC.Kuda );
           list.Add( EC.Nomer_plomby== "" ? null : EC.Nomer_plomby );
           list.Add(EC.Primechanie == "" ? null : EC.Primechanie);
          return list;
      }
      public static List<string> ToListSoder(EventCouch EC)
      {
          var list = new List<string>();
          foreach(var c in EC.Soderzhimoe){
              list.Add(c.ToStringNew());
          }
          return list;
      }
      public static EventCouch ConvertEventWarToEventCouchParent(EventWar e)
        {
            EventCouch EC = new EventCouch();
            EC._rev = e._rev;
            EC._id = e._id;
            EC.archive = e.archive;
            EC.Nomer_upakovki= e.Номер_упаковки;
            EC.Naimenovanie_izdeliya = e.Наименование_изделия;
            EC.Zavodskoj_nomer= e.Заводской_номер;
            EC.Kolichestvo = e.Количество;
            EC.Oboznachenie= e.Обозначение;
            EC.Soderzhimoe = new List<SubEvent>();
            EC.Sistema= e.Система;
            EC.Prinadlezhnost= e.Принадлежность;
             EC.Stoimost = e.Стоимость;
            EC.Otvetstvennyj= e.Ответственный;
            EC.Mestonahozhdenie_na_sklade= e.Местонахождение_на_складе;
            EC.Ves_brutto = e.Вес_брутто;
            EC.Ves_netto = e.Вес_нетто;
            EC.Dlina= e.Длина;
            EC.Shirina = e.Ширина;
            EC.Vysota= e.Высота; 
            EC.Data_priyoma= e.Дата_приёма;
            EC.Otkuda = e.Откуда;
            EC.Data_vydachi= e.Дата_выдачи;
            EC.Kuda= e.Куда;
            EC.Nomer_plomby= e.Номер_пломбы;
            EC.Primechanie = e.Примечание;
            return EC;
        }
      private static SubEvent ConvertEventWarToSubEvent(EventWar e)
        {
            SubEvent SE = new SubEvent();
               
       SE.Naimenovanie_sostavnoj_edinicy = e.Наименование_составной_единицы;
        SE.Oboznachenie_sostavnoj_edinicy =  e.Обозначение_составной_единицы;
         SE.Kolichestvo_sostavnyh_edinic =e.Количество_составных_единиц;
            return SE;
        }
    }
}
