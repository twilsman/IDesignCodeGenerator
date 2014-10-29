using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public interface ILoggerService
    {
        string TestMe(string input);
        void Log(string message, TraceEventType severity, string category);
    }
}
