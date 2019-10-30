using System;
using System.Collections.Generic;
using System.IO;
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

        Byte[] GetAttachmentById(string id, int number);
        //int GetBalance(string userId);

    }
}