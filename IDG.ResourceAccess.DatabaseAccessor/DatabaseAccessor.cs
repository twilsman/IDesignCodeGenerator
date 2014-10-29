using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG.ResourceAccess
{
    public class DatabaseAccessor : IDatabaseAccessor
    {
        private DalGeneratorConfigSection _config = DalGeneratorConfigSection.current;
        private string _connectionString;

        public DatabaseAccessor()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[_config.TargetDbConnectionStringName].ConnectionString;
        }

        public InformationSchemaColumn[] GetColumnsForTable(string tableName, string schemaName)
        {
            InformationSchemaColumn[] columns;

            using (DbContext db = new DbContext(_connectionString))
            {
                string sql = @"
                    select
                        TABLE_CATALOG,
                        TABLE_SCHEMA,
                        TABLE_NAME,
                        COLUMN_NAME,
                        ORDINAL_POSITION,
                        IS_NULLABLE,
                        DATA_TYPE,
                        CAST(COLUMNPROPERTY(object_id(TABLE_SCHEMA + '.' +TABLE_NAME), COLUMN_NAME, 'isidentity') as bit) as IsIdentity
                    from 
                        INFORMATION_SCHEMA.COLUMNS 
                    where 
                        TABLE_NAME = @p0
                        and TABLE_SCHEMA = @p1
                ";

                columns = db.Database.SqlQuery<InformationSchemaColumn>(sql, new SqlParameter("@p0", tableName), new SqlParameter("@p1", schemaName)).ToArray();
            }

            return columns;
        }

        public TablePrimaryKey[] GetPrimaryKeysForTable(string tableName, string schemaName)
        {
            TablePrimaryKey[] keys;

            using(DbContext db = new DbContext(_connectionString))
            {
                string sql = @"sp_pkeys @table_name, @table_owner, @table_qualifier";

                keys = db.Database.SqlQuery<TablePrimaryKey>(sql, 
                                                             new SqlParameter("@table_name", tableName), 
                                                             new SqlParameter("@table_owner", schemaName), 
                                                             new SqlParameter("@table_qualifier", "CCOLAP_Scrub")).ToArray();
            }

            return keys;
        }

        public TableConstraint[] GetForeignKeysForTable(string tableName, string schemaName)
        {
//            TableConstraint[] constraints;

//            using (DbContext db = new DbContext(_connectionString))
//            {
//                string cmd = @"EXEC sp_fkeys
//			            @fktable_name='CommentFact', 
//			            @fktable_owner='olap', 
//			            @fktable_qualifier='CCOLAP_Scrub'";
//                constraints = db.Database.SqlQuery<object>(cmd);
//            }
            return null;
        }
    }
}
