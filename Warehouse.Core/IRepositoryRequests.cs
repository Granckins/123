using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Model;
using Warehouse.Model.Db;

namespace Warehouse.Core
{
    public interface IRepositoryRequests : ICloneable, IDisposable
    {
        ///// <summary>


       
         string GetUUID();
         CouchRequest<EventCouch> GetEventPaginDocuments(int page, int limit, bool archive);
          EventCouch GetEventDocument(string id);
          bool DeleteEventDocument(EventCouch CouchDataSet, string id);
          List<RevsInfo> GetRevisionListEvent(string id, bool flag = true);
        CouchRequest<EventCouch> GetRevisionFiesldsEvent(string id, List<RevsInfo> revs, bool flag=true);
        EventCouch SearchEventByNameAndNumber(string name, string number, Boolean? archive = null);
        CouchRequestMultiKey<EventCouch> FilterByDateDocuments(bool flag, int page, int limit, bool archive, string startkey, string endkey);
        CouchRequestMultiKey<EventCouch> OrderByDateDocuments(bool flag, int page, int limit, bool archive = false);


        //int GetBalance(string userId);

    }
}