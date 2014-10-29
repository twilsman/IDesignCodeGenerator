using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    [ServiceContract]
    public interface IDatabaseAccessor
    {
        [OperationContract]
        InformationSchemaColumn[] GetColumnsForTable(string tableName, string schemaName);

        [OperationContract]
        TablePrimaryKey[] GetPrimaryKeysForTable(string tableName, string schemaName);

        [OperationContract]
        TableConstraint[] GetForeignKeysForTable(string tableName, string schemaName);
    }
}
