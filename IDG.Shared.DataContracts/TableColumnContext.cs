using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public class TableColumnContext
    {
        public string TableNamePlural { get; set; }
        public string TableNameSingular { get; set; }
        public string SchemaName { get; set; }
        public List<TablePrimaryKey> PrimaryKeys { get; set; }
        public List<string> IdentityColumns { get; set; }
        public List<string> AllIds { get; set; }
    }
}
