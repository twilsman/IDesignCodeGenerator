using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using Microsoft.CSharp;
using System.IO;
using System.CodeDom.Compiler;
using System.Configuration;
using RazorEngine;


namespace IDG.Managers
{
    public class DalManager : IDalManager
    {

        public void CreateDataContracts()
        {
            DalGeneratorConfigSection config = DalGeneratorConfigSection.current;

            foreach (IncludedTablesElement table in config.IncludedTables)
            {
                InformationSchemaColumn[] columns = UnityCache.Resolve<IDatabaseAccessor>().GetColumnsForTable(table.Name, table.Schema);

                string tableName = columns.First().TABLE_NAME.TrimEnd(new char[]{'s'});

                UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "DataContract.cshtml", config.DataContractsPath, tableName, columns);
            }        
        }

        public void CreateFluentApiEntries()
        {
            DalGeneratorConfigSection config = DalGeneratorConfigSection.current;

            // Create DbBase.cs File
            UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "DbBase.cshtml", config.AccessorsPath, "DbBase");

            // Create DbContext class
            List<TableColumnContext> contexts = new List<TableColumnContext>();

            foreach (IncludedTablesElement table in DalGeneratorConfigSection.current.IncludedTables)
            {
                List<TablePrimaryKey> primaryKeys = UnityCache.Resolve<IDatabaseAccessor>()
                                                              .GetPrimaryKeysForTable(table.Name, table.Schema)
                                                              .ToList();

                List<InformationSchemaColumn> columns = UnityCache.Resolve<IDatabaseAccessor>()
                                                                  .GetColumnsForTable(table.Name, table.Schema)
                                                                  .ToList();

                List<String> allIdColumns = columns.Where(c => c.COLUMN_NAME.EndsWith("Id"))
                                                   .Where(c => !primaryKeys.Any(k => k.COLUMN_NAME == c.COLUMN_NAME))
                                                   .Select(c => c.COLUMN_NAME).ToList();

                contexts.Add(new TableColumnContext 
                { 
                    TableNamePlural = table.Name,
                    TableNameSingular = table.Name.TrimEnd(new char[]{'s'}),
                    SchemaName = table.Schema,
                    IdentityColumns = columns.Where( c => c.IsIdentity).Select( c => c.COLUMN_NAME).ToList(),
                    PrimaryKeys = primaryKeys,
                    AllIds = allIdColumns
                });
            }

            UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "DbContext.cshtml", config.AccessorsPath, config.DbContextClassName, contexts);
        }

        public void CreateDatabaseAccessors()
        {
            DalGeneratorConfigSection config = DalGeneratorConfigSection.current;

            UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "ITableBaseAccessor.cshtml", config.ContractsPath, "ITableBaseAccessor");

            foreach (IncludedTablesElement table in DalGeneratorConfigSection.current.IncludedTables)
            {
                List<InformationSchemaColumn> columns = UnityCache.Resolve<IDatabaseAccessor>()
                                                                  .GetColumnsForTable(table.Name, table.Schema)
                                                                  .ToList();

                string tableName = columns.First().TABLE_NAME.TrimEnd(new char[]{'s'});
                string accessorFile = string.Format("{0}Accessor", tableName);

                UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "IDatabaseAccessor.cshtml", config.ContractsPath, string.Format("I{0}", accessorFile), tableName);

                UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "DatabaseAccessor.cshtml", config.AccessorsPath, accessorFile, columns);
            }
        }

        public void CreateUnitTests()
        {
            DalGeneratorConfigSection config = DalGeneratorConfigSection.current;

            UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "DatabaseAccessorTestBase.cshtml", config.UnitTestsPath, "DatabaseAccessorTestBase");

            foreach (IncludedTablesElement table in DalGeneratorConfigSection.current.IncludedTables)
            {
                List<InformationSchemaColumn> columns = UnityCache.Resolve<IDatabaseAccessor>()
                                                                  .GetColumnsForTable(table.Name, table.Schema)
                                                                  .ToList();

                string tableName = columns.First().TABLE_NAME.TrimEnd(new char[] { 's' });
                string accessorFile = string.Format("{0}AccessorTests", tableName);

                UnityCache.Resolve<IDalEngine>().GenerateCodeFile(config.TemplatesPath, "DatabaseAccessorUnitTest.cshtml", config.UnitTestsPath, accessorFile, tableName);
            }
        }
    }
}
