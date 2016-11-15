﻿using System;
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
            log.Debug(str);
        }
    }
}
