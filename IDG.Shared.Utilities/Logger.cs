using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDG
{
    public class Logger
    {
        public static ILoggerService LoggerService { get; set; }

        public static void Log(string message, TraceEventType severity, string category)
        {
            Trace.WriteLine(message);

            string disable = ConfigurationManager.AppSettings["DisableLogging"];
            if (string.IsNullOrWhiteSpace(disable) || disable == "false")
            {
                try
                {
                    if (LoggerService == null)
                    {
                        // if someone hasn't overridden the default LoggerService before first Log, create one
                        LoggerService = new LoggerService();
                        LoggerService.Log(message, severity, category);
                    }
                    else
                    {
                        LoggerService.Log(message, severity, category);
                    }
                }
                catch (Exception ex)
                {
                    LoggingFailure("EnterpriseLibrary logging error.", ex);
                }
            }
        }

        public static void Log(string message, TraceEventType severity)
        {
            Log(message, severity, "General");
        }

        private static void LoggingFailure(string msg, Exception ex)
        {
            List<string> extras = new List<string>();
            while (ex != null)
            {
                extras.Add(ex.Message);
                ex = ex.InnerException;
            }

            if (extras.Count > 0)
            {
                msg += String.Format(" (traceback: {0})", String.Join(" / ", extras.ToArray()));
            }

            try
            {
                // write error to Application EventLog
                string src = "TextAnalysis";
                if (!EventLog.SourceExists(src))
                {
                    EventLog.CreateEventSource(src, "Application");
                }
                EventLog.WriteEntry(src, msg);
            }
            catch (Exception)
            {
                // Do not allow the application to possibly stop running because it cannot log.
            }
        }
    }
}
