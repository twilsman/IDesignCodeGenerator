using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public static class TypeMapper 
    { 
        public static Dictionary<string, string> SqlTypes
        {
            get 
            {
                return new Dictionary<string, string>(){
                    {"bigint", "long"},
                    {"bit", "bool"},
                    {"char", "string"},
                    {"date", "DateTime"},
                    {"datetime", "DateTime"},
                    {"datetime2", "DateTime"},
                    {"datetimeoffset", "DateTimeOffset"},
                    {"decimal", "decimal"},
                    {"float", "float"},
                    {"int", "int"},
                    {"nchar", "string"},
                    {"nvarchar", "string"},
                    {"smallint", "int"},
                    {"text", "string"},
                    {"uniqueidentifier", "Guid"},
                    {"varchar", "string"},
                };
            }
        }
    }
}
