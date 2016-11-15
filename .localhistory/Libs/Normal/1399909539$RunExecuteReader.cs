using System;
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
        public static object ExecuteReader<T>(this Database obj, String command)
        {
            Activity wf = new DB.OpenConnection();
            WorkflowInvoker.Invoke(wf,
                new System.Collections.Generic.Dictionary<string, object>
                {
                    {"DBConnectionString",DB.DBParameters.Conn.ConnectionString}
                });

            //wf = new DB.ExecuteReaderCommand();
            //IDictionary<string, object> outputs=WorkflowInvoker.Invoke(wf,
            //    new System.Collections.Generic.Dictionary<string, object>
            //    {
            //        {"SqlCommandString",command}
            //    });

            var result = Libs.DB.ExecuteReaderCommand2.ExecuteReaderTable<T>(command, true);
            //aaa


            return (List<T>)result;
        }
        
    }
}
