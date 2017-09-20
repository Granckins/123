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
        ///// Получает запрос из хранилища по его идентификатор
        ///// </summary>
        ///// <param name="user">Владелец запроса</param>
        ///// <param name="requestId">Идентификатор запроса</param>
        ///// <returns>Запрос</returns>
        //Request GetRequest(UserIdentity user, Guid requestId);
        //RequestModel GetRequestModel(UserIdentity user, Guid requestId);



        ///// <summary>
        ///// Сохраняет запрос в хранилище
        ///// </summary>
        ///// <param name="request">Запрос</param>
        ///// <param name="parentRequestId">Id родительского запроса</param>
        ///// <returns>True, если все успешно</returns>
        //bool SaveRequest(UserIdentity user, Request request, Guid? parentRequestId, int? parnetSourceId, bool? isRefine);


        //void UpdateReponse(UserIdentity userIdentity, Guid requestId, Response resp);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userIdentity"></param>
        ///// <param name="requestId"></param>
        ///// <param name="resp"></param>
        //void AddResponseFromSource(UserIdentity userIdentity, Guid requestId, Response resp);

        //Response GetResponse(string userId, Guid requestId, int sourceId);
        //Dictionary<int, RequestStatus> GetResponseFromSourceStatus(UserIdentity userIdentity, Guid requestId);

        //void SaveRequestStatus(UserIdentity userIdentity, Guid requestId, RequestStatus requestStatus);

        //RequestStatus GetRequestStatus(UserIdentity user, Guid requestId);

        //Dictionary<int, List<int>> GetAvailableSourcesAndProperties(UserIdentity user, List<int> desiredSources);

        //RequestsPage GetRequestsHistory(UserIdentity user, int skip, int take);
        //RequestModel GetRequestsHistoryById(UserIdentity user, string id);
        //List<SourceGroups> GetSourceGroups(UserIdentity user);

        //List<int> GetSourceKeyPropertiesId(UserIdentity user, int sourceId);

        //// Dictionary<int, string> GeAllProperties(UserIdentity user);

        //string GetSourceTitle(UserIdentity user, int sourceId);

        //void SaveCheckedSources(UserIdentity user, List<int> sources);

        //void SaveRequestUiState(UserIdentity user, Guid requestId, bool showSources);


      UserIdentity GetUserIdentityByName(string username);

       List<string> GetUserRoles(UserIdentity user);
      string GetUUID( );
      List<ImportResultResponse> SetDocument(List<EventCouch> CouchDataSet);
      CouchRequest<EventCouch>  GetDocuments(int page, int limit);
        //bool CanRunNewQuery(string userId);

        //void ReduceBalanceOne(string userId);

        //int? IncreaseBalanceOne(string userId);

        //int GetBalance(string userId);

    }
}
