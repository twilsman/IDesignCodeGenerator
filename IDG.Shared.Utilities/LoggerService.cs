using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public class LoggerService : ILoggerService
    {
        public LoggerService()
        {
            LogWriterFactory logWriterFactory = new LogWriterFactory();
            var logWriter = logWriterFactory.Create();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.SetLogWriter(logWriter, false);
        }

        public string TestMe(string input)
        {
            return input + ", Logger";
        }

        /// <summary>
        /// log to a custom Category
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severity"></param>
        /// <param name="category"></param>
        public void Log(string message, TraceEventType severity, string category)
        {
            var logEntry = new LogEntry();
            logEntry.Categories.Add(category);
            logEntry.Severity = severity;
            logEntry.Message = message;
            logEntry.TimeStamp = DateTime.Now;
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);
        }
    }
}
