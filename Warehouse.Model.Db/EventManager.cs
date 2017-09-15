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
                            EC.Содержимое.Add(ConvertEventWarToSubEvent(e));
                        }

                    }

                    i++;
                }
            }

            return EC;
        }
      private static EventCouch ConvertEventWarToEventCouchParent(EventWar e)
        {
            EventCouch EC = new EventCouch();
            EC.Номер_упаковки = e.Номер_упаковки;
            EC.Наименование_изделия = e.Наименование_изделия;
            EC.Заводской_номер = e.Заводской_номер;
            EC.Количество = e.Количество;
            EC.Обозначение = e.Обозначение;
            EC.Содержимое = new List<SubEvent>();
            EC.Система = e.Система;
            EC.Принадлежность = e.Принадлежность;
            EC.Принадлежность_к_объекту = e.Принадлежность_к_объекту;
            EC.Стоимость = e.Стоимость;
            EC.Ответственный = e.Ответственный;
            EC.Местонахождение_на_складе = e.Местонахождение_на_складе;
            EC.Вес_брутто = e.Вес_брутто;
            EC.Вес_нетто = e.Вес_нетто;
            EC.Длина = e.Длина;
            EC.Ширина = e.Ширина;
            EC.Высота = e.Высота;
            EC.Номер_контейнера = e.Номер_контейнера;
            EC.Номер_упаковочного_ящика = e.Номер_упаковочного_ящика;
            EC.Дата_приёма = e.Дата_приёма;
            EC.Откуда = e.Откуда;
            EC.Дата_выдачи = e.Дата_выдачи;
            EC.Куда = e.Куда;
            EC.Номер_пломбы = e.Номер_пломбы;
            EC.Примечание = e.Примечание;
            return EC;
        }
      private static SubEvent ConvertEventWarToSubEvent(EventWar e)
        {
            SubEvent SE = new SubEvent();
            SE.Наименование_составной_единицы = e.Наименование_составной_единицы;
            SE.Обозначение_составной_единицы = e.Обозначение_составной_единицы;
            SE.Количество_составных_единиц = e.Количество_составных_единиц;
            return SE;
        }
    }
}
