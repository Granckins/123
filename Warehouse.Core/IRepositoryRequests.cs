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
   

      UserIdentity GetUserIdentityByName(string username);

       List<string> GetUserRoles(UserIdentity user);
      string GetUUID( );
      List<ImportResultResponse> SetEventDocuments(List<EventCouch> CouchDataSet, string user);
      CouchRequest<EventCouch>  GetEventPaginDocuments(int page, int limit,bool archive);
      CouchRequest<EventCouch> GetFilterSortDocuments(int page, int limit, bool archive, FilterSort FS = null);
      EventCouch GetEventDocument(string id);
      List<ImportResultResponse> SetEventDocument(EventCouch CouchDataSet, string user, string id);
      bool DeleteEventDocument(EventCouch CouchDataSet, string id);
       List<RevsInfo> GetRevisionListEvent(string id);
      CouchRequest<EventCouch> GetRevisionFiesldsEvent(string id, List<RevsInfo> revs);
      EventCouch SearchEventByNameAndNumber(string name, string number, Boolean? archive = null);
      CouchRequestMultiKey<EventCouch> FilterByDateDocuments(bool flag, int page, int limit, bool archive, string startkey, string endkey);
      CouchRequestMultiKey<EventCouch> OrderByDateDocuments(bool flag, int page  , int limit , bool archive = false);
     

        //int GetBalance(string userId);

    }
}
