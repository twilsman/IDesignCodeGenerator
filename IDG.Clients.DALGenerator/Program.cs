using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDG;
using System.Configuration;
using IDG.ResourceAccess;

namespace IDG.Clients.DALGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            DalGeneratorConfigSection config = DalGeneratorConfigSection.current;

            UnityCache.Resolve<IDalManager>().CreateDataContracts();
            UnityCache.Resolve<IDalManager>().CreateFluentApiEntries();
            UnityCache.Resolve<IDalManager>().CreateDatabaseAccessors();
            UnityCache.Resolve<IDalManager>().CreateUnitTests();
        }
    }
}
