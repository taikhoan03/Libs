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


            //var aaa = new DB.ExecuteReaderCommand2();
            //aaa.SqlCommandString = command;
            Libs.DB.ExecuteReaderCommand2.ExecuteReaderTable(command, true);
            //aaa





            wf = new DB.CloseConnection();
            WorkflowInvoker.Invoke(wf);

            //var result = aaa.Execute<Test>();
            
            //if (result == null)
            //    return null;
            //var type = typeof(T);

            ////var instanceOfClass = default(T);
            ////var constructor = type.GetConstructor(Type.EmptyTypes);
            ////var parameters = new object[0];
            ////var instanceOfClass = constructor.Invoke(parameters);
            //var instanceOfClass = type.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
            //return merge(result, instanceOfClass);
            //return instanceOfClass;
            return null;
        }
        
    }
}
