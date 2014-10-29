using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace IDG
{
    [ServiceContract]
    public interface IDalManager
    {
        [OperationContract]
        void CreateDataContracts();

        [OperationContract]
        void CreateFluentApiEntries();

        [OperationContract]
        void CreateDatabaseAccessors();

        [OperationContract]
        void CreateUnitTests();
    }
}
