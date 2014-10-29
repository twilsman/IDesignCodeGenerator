using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public class TablePrimaryKey
    {
        public string TABLE_NAME { get; set; }
        public string PK_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public short KEY_SEQ { get; set; }
    }
}
