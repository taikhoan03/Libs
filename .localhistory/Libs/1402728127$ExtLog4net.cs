using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Libs
{
    public static class ExtLog4net
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod
().DeclaringType);
        public static void LogDebug(string str,Exception ex=null)
        {
            if (ex == null)
            {
                log.Debug(str);
            }
            else
            {
                log.Debug(str,ex);
            }
        }
        public static void Debug(this ILog log, Func<string> formattingCallback)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(formattingCallback());
            }
        }
        public static void Info(this ILog log, Func<string> formattingCallback)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(formattingCallback());
            }
        }
        public static void Warn(this ILog log, Func<string> formattingCallback)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(formattingCallback());
            }
        }
        public static void Error(this ILog log, Func<string> formattingCallback)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(formattingCallback());
            }
        }
        public static void Fatal(this ILog log, Func<string> formattingCallback)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(formattingCallback());
            }
        }
    }
}
