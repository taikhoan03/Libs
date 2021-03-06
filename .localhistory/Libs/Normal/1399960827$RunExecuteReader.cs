﻿using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using Libs;
using System.Reflection;
namespace Libs
{
    public class Test
    {
        public int ID { get; set; }
        public string username { get; set; }
    }
    public static class ExtMethods//:System.Activities.Activity
    {
        public static object ExecuteReader<T>(this Database obj, String command, bool CloseConnectionOnDone)
        {
            Activity wf = new DB.OpenConnection();
            WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"DBConnectionString",DB.DBParameters.Conn.ConnectionString}
                });

            wf = new DB.ExecuteReaderCommand();
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"SqlCommandString",command},
                    {"InputType",new Test()}
                });
            var result = (List<Test>)outputs["ListRecords"];
            //var result = Libs.DB.ExecuteReaderCommand2.ExecuteReaderTable<T>(command, CloseConnectionOnDone);
            //Simple type
            if (result.GetType() == typeof(T))
                return result;

            return (List<T>)result;
        }
        
    }
}
