using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Model;
using Warehouse.Model.Db;

namespace Warehouse.Core
{
    public interface IRepositoryRequestsUnits : ICloneable, IDisposable
    {
        ///// <summary>





        CouchRequest<Unit> GetUnits();


        //int GetBalance(string userId);

    }
}